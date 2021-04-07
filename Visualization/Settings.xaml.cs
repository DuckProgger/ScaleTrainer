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
using System.Reflection;

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
        private List<string> instrumentList;
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
            instrumentType = FindTypeByNameAttribute(instrumentName);
            main.SelectedInstrument = instrumentType;
            if (Strings != null)
                GetStringNumberList(instrumentType.Name);
            main.InvokeParameterChangedEvent();
        }

        private Type FindTypeByNameAttribute(string name)
        {            
            Type[] types = GetSubclasses(typeof(StringedInstrument));
            var typesWithNameAttr = from type in types
                                    let attr = (NameAttribute)type.GetCustomAttribute(typeof(NameAttribute))
                                    where attr.Name == name
                                    select type;
            foreach (Type item in typesWithNameAttr)
            {
                return item;
            }
            throw new NotImplementedException();
        }

        private Type[] GetSubclasses(Type baseType)
        {
            List<Type> typeList = new List<Type>(3);
            Type[] types = Assembly.GetAssembly(baseType).GetTypes();
            var derivedTypes = from type in types
                               where type.IsSubclassOf(baseType)
                               select type;
            foreach (Type item in derivedTypes)
            {
                typeList.Add(item);
            }
            return typeList.ToArray();
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
            main.selectedKey = (Note.NoteName)((ComboBox)sender).SelectedItem;
            main.InvokeParameterChangedEvent();
        }

        private void GetInstrumentList()
        {
            instrumentList = new List<string>(3);
            Type[] derivedTypes = GetSubclasses(typeof(StringedInstrument));
            foreach (Type type in derivedTypes)
            {
                NameAttribute attr = (NameAttribute)type.GetCustomAttribute(typeof(NameAttribute));
                if(attr != null) 
                    instrumentList.Add(attr.Name);
            }                        
            Instrument.ItemsSource = instrumentList.ToArray();
        }

        private void GetScaleList()
        {
            Scales.ItemsSource = DataExchange.GetScaleListFromXml();
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
                Key.Items.Add(key);
            }
        }
    }
}
