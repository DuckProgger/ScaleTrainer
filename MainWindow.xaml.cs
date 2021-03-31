using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System;

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
        int fret = 0;
        double[] k;
        event EventHandler ParameterChanged;
        private readonly int maxFrets = 24;

        public MainWindow()
        {
            InitializeComponent();
            CreateNeckColumns(maxFrets);
            ParameterChanged += TryCreateVisualization;
            Key.Items.Add(Note.NoteName.D);
            Key.Items.Add(Note.NoteName.C);
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

        private void Scale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            selectedScale = comboBoxItem.Content.ToString();
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedKey = (Note.NoteName)((ComboBox)sender).SelectedItem;
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void TryCreateVisualization(object sender, EventArgs e)
        {
            try
            {
                CreateVisualization();
            }
            catch (Exception ex)
            {
                Information.Text = ex.Message;
            }
        }

        private void CreateNeckColumns(int number)
        {
            k = new double[selectedFrets.Value];
            CalcKoeff(1.0);
            for (int i = 0; i < number; i++)
            {
                Neck.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(k[i], GridUnitType.Star)
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

        private void CreateFretsOnNeck(int strings, int frets)
        {
            for (int i = 0; i < strings; i++)
            {
                for (int j = 1; j <= frets; j++)
                {
                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 10,
                        Height = 10,
                        Fill = new SolidColorBrush(Colors.Red),
                    };
                    if (guitarVis.AvailableFrets[i, j])
                    {
                        Neck.Children.Add(ellipse);
                        Grid.SetColumn(ellipse, j - 1);
                        Grid.SetRow(ellipse, i);
                    }
                }
            }
        }

        private void CreateFretsOnNut(int strings)
        {
            for (int i = 0; i < strings; i++)
            {
                Ellipse ellipse = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Red),
                };
                if (guitarVis.AvailableFrets[i, 0])
                {
                    Nut.Children.Add(ellipse);
                    Grid.SetRow(ellipse, i);
                }

            }
        }

        private void CreateNutRows(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Nut.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void CreateVisualization()
        {
            if (selectedStrings.HasValue && selectedFrets.HasValue && selectedTuning != null && selectedScale != null && selectedKey != 0)
            {
                guitar = new Guitar(selectedStrings.Value, selectedFrets.Value, selectedTuning);
                scale = new Scale(selectedScale, selectedKey);
                guitarVis = new StringedVisualisation(guitar, scale);
                ClearNeck();
                CreateNeckRows(selectedStrings.Value);
                CreateNutRows(selectedStrings.Value);
                CreateFretsOnNeck(selectedStrings.Value, selectedFrets.Value);
                CreateFretsOnNut(selectedStrings.Value);
                Information.Text = "";
            }
        }

        void CalcKoeff(double value)
        {
            if (fret < selectedFrets.Value)
            {
                double temp = value / Math.Pow(2, 0.083333);
                k[fret] = value - temp;
                fret++;
                CalcKoeff(temp);
            }
            return;
        }

        void ClearNeck()
        {
            if (Neck != null)
            {
                Neck.RowDefinitions.Clear();
                Neck.Children.Clear();
                Nut.RowDefinitions.Clear();
                Nut.Children.Clear();
            }
        }
    }
}
