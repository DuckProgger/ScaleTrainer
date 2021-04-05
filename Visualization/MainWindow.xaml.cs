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
        public MainWindow()
        {
            InitializeComponent();
            CreateNeckColumns(maxFrets);
            settingsWindow = new Settings();
            ParameterChanged += TryCreateVisualization;
        }

        internal int? SelectedStrings { get; set; }
        internal int? SelectedFrets { get; set; }
        internal Type SelectedInstrument { get; set; }
        internal string SelectedTuning { get; set; }
        internal string SelectedScale { get; set; }
        internal Note.NoteName selectedKey;

        private StringedInstrument Instrument { get; set; }
        private Scale scale;
        private StringedVisualisation guitarVis;
        private int fret = 0;
        private double[] fretRanges;
        private readonly int maxFrets = 24;
        private Settings settingsWindow;

        public event EventHandler ParameterChanged;

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
                switch (SelectedInstrument.Name)
                {
                    case nameof(Guitar):
                        Instrument = new Guitar(SelectedStrings.Value, SelectedFrets.Value, SelectedTuning);
                        break;
                    case nameof(Bass):
                        Instrument = new Bass(SelectedStrings.Value, SelectedFrets.Value, SelectedTuning);
                        break;
                }               
                scale = new Scale(SelectedScale, selectedKey);
                guitarVis = new StringedVisualisation(Instrument, scale);
                ClearNeck();
                CreateNeckRows(SelectedStrings.Value);
                CreateNutRows(SelectedStrings.Value);
                CreateFretsOnNeck(SelectedStrings.Value, SelectedFrets.Value);
                CreateFretsOnNut(SelectedStrings.Value);
            }
        }

        public void InvokeParameterChangedEvent()
        {
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool AllParametersSet()
        {
            return SelectedInstrument != null && SelectedStrings.HasValue && SelectedFrets.HasValue &&
                SelectedTuning != null && SelectedScale != null && selectedKey != 0;
        }

        private void CreateNeckColumns(int number)
        {
            fretRanges = new double[number];
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
            if (fret < maxFrets)
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!settingsWindow.IsActive)
            {
                settingsWindow.Show();
            }
            else settingsWindow.Activate();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            Height = Math.Pow(Width, 0.725);
        }
    }
}
