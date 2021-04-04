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
        MainWindow main;

        public Settings()
        {
            main = GetMainWindowObj();
            InitializeComponent();
            GetScaleList();
            Key.Items.Add(Note.NoteName.D);
            Key.Items.Add(Note.NoteName.C);
        }

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

        private void Strings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
            {
                main.selectedStrings = temp;
            }
            main.InvokeParameterChangedEvent();
        }

        private void Frets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (main == null) return;
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
            {
                main.selectedFrets = temp;
            }

            main.InvokeParameterChangedEvent();
        }

        private void Tuning_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (main == null) return;
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            main.selectedTuning = comboBoxItem.Content.ToString();
            main.InvokeParameterChangedEvent();
        }

        private void Scales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (main == null) return;
            main.selectedScale = (string)((ListBox)sender).SelectedItem;
            main.InvokeParameterChangedEvent();
        }

        private void Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (main == null) return;
            main.selectedKey = (Note.NoteName)((ComboBox)sender).SelectedItem;
            main.InvokeParameterChangedEvent();
        }

        private void GetScaleList()
        {
            Scales.ItemsSource = DataExchange.GetScaleListFromXml(); ;
        }
    }
}
