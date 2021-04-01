using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Scale_Trainer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int? selectedStrings, selectedFrets;
        private string selectedTuning, selectedScale;
        private Note.NoteName selectedKey;
        private Guitar guitar;
        private Scale scale;
        private StringedVisualisation guitarVis;
        private int fret = 0;
        private double[] fretRanges;

        private event EventHandler ParameterChanged;
        private readonly int maxFrets = 24;

        public MainWindow()
        {
            InitializeComponent();
            CreateNeckColumns(maxFrets);
            GetScaleList();
            ParameterChanged += TryCreateVisualization;
            Key.Items.Add(Note.NoteName.D);
            Key.Items.Add(Note.NoteName.C);
        }

        private void TryCreateVisualization(object sender, EventArgs e)
        {
            try
            {
                CreateVisualization();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateVisualization()
        {
            if (AllParametersSet())
            {
                guitar = new Guitar(selectedStrings.Value, selectedFrets.Value, selectedTuning);
                scale = new Scale(selectedScale, selectedKey);
                guitarVis = new StringedVisualisation(guitar, scale);
                ClearNeck();
                CreateNeckRows(selectedStrings.Value);
                CreateNutRows(selectedStrings.Value);
                CreateFretsOnNeck(selectedStrings.Value, selectedFrets.Value);
                CreateFretsOnNut(selectedStrings.Value);
            }
        }

        private bool AllParametersSet()
        {
            return selectedStrings.HasValue && selectedFrets.HasValue && selectedTuning != null && selectedScale != null && selectedKey != 0;
        }

        private void CreateNeckColumns(int number)
        {
            fretRanges = new double[selectedFrets.Value];
            CalcFretRanges(1.0);
            for (int i = 0; i < number; i++)
            {
                Neck.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(fretRanges[i], GridUnitType.Star)
                });
            }
        }

        private void CreateNeckRows(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Neck.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void CreateNutRows(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Nut.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void CreateFretsOnNeck(int strings, int frets)
        {
            for (int i = 0; i < strings; i++)
            {
                for (int j = 1; j <= frets; j++)
                {
                    if (guitarVis.AvailableFrets[i, j])
                    {
                        Grid fret = CreateFret(j);
                        Neck.Children.Add(fret);
                        Grid.SetColumn(fret, j - 1);
                        Grid.SetRow(fret, i);
                    }
                }
            }
        }

        private void CreateFretsOnNut(int strings)
        {
            for (int i = 0; i < strings; i++)
            {
                if (guitarVis.AvailableFrets[i, 0])
                {
                    Grid fret = CreateFret(0);
                    Nut.Children.Add(fret);
                    Grid.SetRow(fret, i);
                }
            }
        }

        private Grid CreateFret(int number)
        {
            Ellipse ellipse = new Ellipse()
            {
                Style = (Style)TryFindResource("Fret")
            };
            TextBlock numberText = new TextBlock()
            {
                Text = number.ToString(),
                Style = (Style)TryFindResource("FretNumber")
            };
            Grid fret = new Grid();
            fret.Children.Add(ellipse);
            fret.Children.Add(numberText);
            return fret;
        }

        private void CalcFretRanges(double value)
        {
            if (fret < selectedFrets.Value)
            {
                double temp = value / Math.Pow(2, 0.083333);
                fretRanges[fret] = value - temp;
                fret++;
                CalcFretRanges(temp);
            }
        }

        private void ClearNeck()
        {
            if (Neck != null)
            {
                Neck.RowDefinitions.Clear();
                Neck.Children.Clear();
                Nut.RowDefinitions.Clear();
                Nut.Children.Clear();
            }
        }

        private void Strings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
            {
                selectedStrings = temp;
            }
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Frets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
            {
                selectedFrets = temp;
            }

            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Tuning_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            selectedTuning = comboBoxItem.Content.ToString();
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Scales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedScale = (string)((ListBox)sender).SelectedItem;
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedKey = (Note.NoteName)((ComboBox)sender).SelectedItem;
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void GetScaleList()
        {             
            Scales.ItemsSource = DataExchange.GetScaleListFromXML(); ;
        }
    }
}
