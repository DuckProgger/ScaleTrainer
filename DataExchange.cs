using System.IO;
using System.Xml;

namespace Scale_Trainer
{
    internal static class DataExchange
    {
        public static Notes[] GetTuningFromXML(StringedConfig instrument, Tuning.TuningName tuning)
        {
            Notes[] notes = new Notes[instrument.Strings];
            string strInstrument = instrument.GetType().Name;
            string strTuning = tuning.ToString();
            string strStrings = instrument.Strings.ToString();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"data/tunings.xml");
            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;

            // обход всех узлов в корневом элементе
            foreach (XmlNode xnode in xRoot)
            {
                // получаем атрибуты
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode[] attr = new XmlNode[3];
                    attr[0] = xnode.Attributes.GetNamedItem("instrument");
                    attr[1] = xnode.Attributes.GetNamedItem("strings");
                    attr[2] = xnode.Attributes.GetNamedItem("name");

                    if (attr[2].Value == strTuning && attr[1].Value == strStrings && attr[0].Value == strInstrument)
                    {
                        // проверка совпадения атрибута количества струн в XML количеству полей этого узла 
                        Validate.IsTrue(xnode.ChildNodes.Count == int.Parse(attr[1].Value) * 2, "Несоответствие количества струн в атрибуте и количества полей.");
                        int nodePos = 0;
                        // обходим все дочерние узлы элемента
                        for (int stringNote = 0; stringNote < xnode.ChildNodes.Count / 2; stringNote++, nodePos += 2)
                        {
                            Notes.NoteName note = Notes.StringToNote(xnode.ChildNodes[nodePos].InnerText);
                            // попытаться преобразовать полученное поле октавы из строки в байт
                            Validate.IsTrue(byte.TryParse(xnode.ChildNodes[nodePos + 1].InnerText, out byte octave), "Файл содержит недопустимый символ.");
                            notes[stringNote] = new Notes(note, octave);
                        }
                        return notes;
                    }
                }
            }
            throw new FileNotFoundException();
        }
    }
}
