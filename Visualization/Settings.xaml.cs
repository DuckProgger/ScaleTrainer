using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scale_Trainer
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            main = GetMainWindowObj();
            InitializeComponent();
            GetScaleList();
            GetTuningList(ConvertInstrumentName(instrumentName));
            GetKeyList();
        }

        private readonly MainWindow main;
        private string instrumentName;

        private MainWindow GetMainWindowObj()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    return window as MainWindow;
                }
            }
            return null;
        }

        private void Instrument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            instrumentName = comboBoxItem.Content.ToString();
            switch (instrumentName)
            {
                case "Гитара":
                    main.SelectedInstrument = typeof(Guitar);
                    break;
                case "Бас гитара":
                    main.SelectedInstrument = typeof(Bass);
                    break;
            }
            main.InvokeParameterChangedEvent();
        }

        public static string ConvertInstrumentName(string rusName)
        {
            switch (rusName)
            {
                case "Гитара":
                    return "Guitar";
                case "Бас гитара":
                    return "Bass";
            }
            throw new NotImplementedException();
        }

        private void Strings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
            {
                main.SelectedStrings = temp;
            }
            main.InvokeParameterChangedEvent();
        }

        private void Frets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
            {
                main.SelectedFrets = temp;
            }
            main.InvokeParameterChangedEvent();
        }

        private void Tuning_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string comboBoxItem = (string)((ComboBox)sender).SelectedItem;
            main.SelectedTuning = comboBoxItem;
            main.InvokeParameterChangedEvent();
        }

        private void Scales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            main.SelectedScale = (string)((ListBox)sender).SelectedItem;
            main.InvokeParameterChangedEvent();
        }

        private void Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            main.selectedKey = (Note.NoteName)((ComboBox)sender).SelectedItem;
            main.InvokeParameterChangedEvent();
        }

        private void GetScaleList()
        {
            Scales.ItemsSource = DataExchange.GetScaleListFromXml();
        }

        private void GetTuningList(string engName)
        {
            Tuning.ItemsSource = DataExchange.GetTuningNamesFromXml(engName);
        }

        private void GetKeyList()
        {
            for (Note.NoteName key = Note.NoteName.C; key <= Note.NoteName.B; key++)
            {
                Key.Items.Add(key);
            }
        }
    }
}
