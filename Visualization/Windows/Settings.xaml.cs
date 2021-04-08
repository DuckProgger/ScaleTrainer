using System;
using System.Collections.Generic;
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
            GetInstrumentList();
            GetScaleList();
            GetStringNumberList(instrumentType.Name);
            GetTuningList(instrumentType.Name, strings);
            GetKeyList();            
        }

        private readonly MainWindow main;
        private string instrumentName;
        private string strings;
        private Type instrumentType;

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
            instrumentName = (string)((ComboBox)sender).SelectedItem;           
            instrumentType = Util.FindTypeByNameAttribute(instrumentName);
            main.SelectedInstrument = instrumentType;
            if (Strings != null)
                GetStringNumberList(instrumentType.Name);
            main.InvokeParameterChangedEvent();
        }        

        private void Strings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            strings = (string)((ComboBox)sender).SelectedItem;
            if (int.TryParse(strings, out int temp))
            {
                main.SelectedStrings = temp;
            }
            GetTuningList(instrumentType.Name, strings);
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
            main.SelectedTuning = (string)((ComboBox)sender).SelectedItem;           
            main.InvokeParameterChangedEvent();
        }

        private void Scales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            main.SelectedScale = (string)((ListBox)sender).SelectedItem;
            main.InvokeParameterChangedEvent();
        }

        private void Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (string)((ComboBox)sender).SelectedItem;
            main.selectedKey = Note.StringToNoteName(item);
            main.InvokeParameterChangedEvent();
        }

        private void GetInstrumentList()
        {
            Instrument.ItemsSource = Util.GetNameAttributes();
        }

        private void GetScaleList()
        {
            Scales.ItemsSource = DataExchange.GetScaleListFromXml();
            Scales.SelectedItem = 1;
        }

        private void GetTuningList(string engName, string strings)
        {
            Tuning.ItemsSource = DataExchange.GetTuningNamesFromXml(engName, strings);
            Tuning.SelectedIndex = 0;
        }

        private void GetStringNumberList(string instrument)
        {
            Strings.ItemsSource = DataExchange.GetStringNumberFromXml(instrument);
            Strings.SelectedIndex = 0;
        }

        private void GetKeyList()
        {
            for (Note.NoteName key = Note.NoteName.C; key <= Note.NoteName.B; key++)
            {
                string strKey = key.ToString().Replace("_sh", "#");
                Key.Items.Add(strKey);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }
    }
}
