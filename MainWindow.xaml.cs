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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scale_Trainer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int? selectedStrings, selectedFrets;
        string selectedTuning, selectedScale;
        Note.NoteName selectedKey;
        Guitar guitar;
        Scale scale;
        StringedVisualisation giutarVis;

        public MainWindow()
        {
            InitializeComponent();
            Key.Items.Add(Note.NoteName.D);
            Key.Items.Add(Note.NoteName.C);

        }

        private void Strings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
                selectedStrings = temp;
            TryCreateVisualization();
        }

        private void Frets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            if (int.TryParse(comboBoxItem.Content.ToString(), out int temp))
                selectedFrets = temp;
            TryCreateVisualization();
        }

        private void Tuning_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            selectedTuning = comboBoxItem.Content.ToString();
            TryCreateVisualization();
        }

        private void Scale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)((ComboBox)sender).SelectedItem;
            selectedScale = comboBoxItem.Content.ToString();
            TryCreateVisualization();
        }

        private void Key_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedKey = (Note.NoteName)((ComboBox)sender).SelectedItem;
            TryCreateVisualization();
        }

        private void TryCreateVisualization()
        {
            if (selectedStrings.HasValue && selectedFrets.HasValue && selectedTuning != null && selectedScale != null && selectedKey != 0)
            {
                guitar = new Guitar(selectedStrings.Value, selectedFrets.Value, selectedTuning);
                scale = new Scale(selectedScale, selectedKey);
                giutarVis = new StringedVisualisation(guitar, scale);
            }
        }
    }
}
