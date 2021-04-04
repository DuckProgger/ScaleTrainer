using System;
using System.Xml;

namespace Scale_Trainer
{
    internal static class DataExchange
    {
        private static readonly string tuningPath = @"data/tunings.xml";
        private static readonly string scalePath = @"data/scales.xml";

        public static Note[] GetTuningFromXml(StringedInstrument instrument, string tuningName)
        {
            Note[] notes = new Note[instrument.Strings];
            string strInstrument = instrument.GetType().Name;
            string strTuning = tuningName;
            string strStrings = instrument.Strings.ToString();

            // сформировать запрос xPath
            string xPath = string.Format("tuning[@instrument='{0}' and @strings='{1}' and @name='{2}']", strInstrument, strStrings, strTuning);

            // найти узел в файле XML
            XmlNode xNode = GetNodeByXpath(tuningPath, xPath);

            //проверка совпадения атрибута количества струн в XML количеству полей этого узла
            Validate.IsTrue(xNode.ChildNodes.Count == int.Parse(xNode.Attributes.GetNamedItem("strings").Value) * 2,
                "Несоответствие количества струн в атрибуте и количества полей.");

            int nodePos = 0;
            // обходим все дочерние узлы элемента
            for (int stringNote = 0; stringNote < xNode.ChildNodes.Count / 2; stringNote++, nodePos += 2)
            {
                Note.NoteName note = Note.StringToNoteName(xNode.ChildNodes[nodePos].InnerText);
                // попытаться преобразовать полученное поле октавы из строки в байт
                Validate.IsTrue(byte.TryParse(xNode.ChildNodes[nodePos + 1].InnerText, out byte octave), "Файл содержит недопустимый символ.");
                notes[stringNote] = new Note(note, octave);
            }
            return notes;
        }
               
        public static string[] GetScaleListFromXml()
        {
            XmlNodeList list = GetNodesByXpath(scalePath, "scale[@name]");
            string[] strList = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                strList[i] = list.Item(i).Attributes.GetNamedItem("name").Value;
            }

            return strList;
        }

        public static string[] GetTuningNamesFromXml(StringedInstrument instrument)
        {
            string[] tuningNames;
            XmlNodeList list = GetNodesByXpath(scalePath, "tuning[@name]");
            string strInstrument = instrument.GetType().Name;

        }

        public static bool TryFindScale(string name, out Scale scale)
        {
            try
            {
                XmlNode node = GetNodeByXpath(scalePath, string.Format("scale[@name='{0}']", name));
                int[] intervals = new int[node.ChildNodes.Count];

                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    intervals[i] = int.Parse(node.ChildNodes[i].InnerText);
                }

                scale = new Scale(node.Attributes.GetNamedItem("name").Value, intervals);
            }
            catch (Exception)
            {
                scale = null;
                return false;
            }
            
            return true;
        }

        private static XmlNode GetNodeByXpath(string xmlPath, string xPath)
        {
            // получить файл XML
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPath);

            // получить корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;

            // найти узел по запросу xPath
            XmlNodeList xNodes = xRoot.SelectNodes(xPath);

            // запись не найдена
            Validate.IsFalse(xNodes.Count == 0, "Запись не найдена в файле XML.");

            // проверить на уникальность
            Validate.IsFalse(xNodes.Count > 1, "В файле XML имеется несколько одинаковых записей.");

            // извлечь найденный узел
            return xNodes[0];
        }

        private static XmlNodeList GetNodesByXpath(string xmlPath, string xPath)
        {
            // получить файл XML
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPath);

            // получить корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;

            // найти узел по запросу xPath
            XmlNodeList xNodes = xRoot.SelectNodes(xPath);

            return xNodes;
        }
    }
}
