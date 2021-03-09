using System.IO;
using System.Collections;

namespace Scale_Trainer
{
    internal class Scale : IEnumerable, IEnumerator
    {
        public string Name { get; private set; }
        public Note[] ScaleNotes { get; private set; }
        public Note.NoteName Key { get; private set; }
        private int[] intervals;
        //private int curIntervalPos = -1;
        private int curNotePos = -1;

        public Scale(string name, int[] intervals, Note.NoteName key = Note.NoteName.C)
        {
            Name = name;
            ScaleNotes = new Note[intervals.Length];
            this.intervals = intervals;
            Key = key;
            InitializeScaleNotes();
        }

        public Scale(string name, Note.NoteName key)
        {
            if (DataExchange.TryFindScale(name, out Scale newScale))
            {
                Name = newScale.Name;
                ScaleNotes = newScale.ScaleNotes;
                intervals = newScale.intervals;
            }
            else
            {
                throw new FileNotFoundException("Гамма не найдена.");
            }
            Key = key;
        }

        public Scale(Scale scale)
        {
            Name = scale.Name;
            ScaleNotes = new Note[scale.intervals.Length];
            for (int i = 0; i < ScaleNotes.Length; i++)
            {
                ScaleNotes[i] = new Note(scale.ScaleNotes[i]);
            }
            intervals = scale.intervals;
            Key = scale.Key;
        }

        private void InitializeScaleNotes()
        {
            ScaleNotes[0] = new Note(Key);
            for (int i = 1; i < ScaleNotes.Length; i++)
            {
                ScaleNotes[i] = new Note(ScaleNotes[i - 1]);
                ScaleNotes[i].OffsetNote(intervals[i - 1]);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public object Current
        {
            get
            {
                return ScaleNotes[curNotePos];
            }
        }

        public bool MoveNext()
        {
            if (curNotePos == ScaleNotes.Length - 1)
            {
                Reset();
                return false;
            }
            curNotePos++;
            return true;
        }

        public void Reset()
        {
            curNotePos = -1;
        }

        public Note GetCurrentNote()
        {
            if (curNotePos < 0)
            {
                curNotePos = 0;
            }
            return ScaleNotes[curNotePos];
        }








        //public string Name { get; private set; }
        //public int[] Intervals { get; private set; }
        //public Note.NoteName Key { get; private set; }
        //private int curPos = -1;

        //public Scale(string name, int[] intervals, Note.NoteName key = Note.NoteName.C)
        //{
        //    Name = name;
        //    Intervals = intervals;
        //    Key = key;            
        //}

        //public Scale(string name, Note.NoteName key)
        //{
        //    if (DataExchange.TryFindScale(name, out Scale newScale))
        //    {
        //        Name = newScale.Name;
        //        Intervals = newScale.Intervals;
        //    }
        //    else
        //    {
        //        throw new FileNotFoundException("Гамма не найдена.");
        //    }
        //    Key = key;
        //}  

        //public int MoveNext()
        //{
        //    curPos++;
        //    if (curPos > Intervals.Length - 1)
        //    {
        //        curPos = 0;
        //    }
        //    return Intervals[curPos];
        //}

        //public int MovePrevious()
        //{
        //    curPos--;
        //    if (curPos < 0)
        //    {
        //        curPos = Intervals.Length - 1;
        //    }
        //    return Intervals[curPos];
        //}

        //public Note.NoteName GetCurrentNoteName()
        //{
        //    return Key
        //}
    }
}
