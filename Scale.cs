using System.IO;
using System.Collections;

namespace Scale_Trainer
{
    internal class Scale
    {
        public string Name { get; private set; }
        public int[] Intervals { get; private set; }
        public Note.NoteName Key { get; private set; }
        private int curPos = -1;

        public Scale(string name, int[] intervals, Note.NoteName key = Note.NoteName.C)
        {
            Name = name;
            Intervals = intervals;
            Key = key;            
        }

        public Scale(string name, Note.NoteName key)
        {
            if (DataExchange.TryFindScale(name, out Scale newScale))
            {
                Name = newScale.Name;
                Intervals = newScale.Intervals;
            }
            else
            {
                throw new FileNotFoundException("Гамма не найдена.");
            }
            Key = key;
        }  

        public int MoveNext()
        {
            curPos++;
            if (curPos > Intervals.Length - 1)
            {
                curPos = 0;
            }
            return Intervals[curPos];
        }

        public int MovePrevious()
        {
            curPos--;
            if (curPos < 0)
            {
                curPos = Intervals.Length - 1;
            }
            return Intervals[curPos];
        }
    }
}
