using System;

namespace Scale_Trainer
{
    internal class Note
    {
        public NoteName CurNote { get; set; }
        public enum NoteName : byte { C = 1, C_sh = 2, D = 3, D_sh = 4, E = 5, F = 6, F_sh = 7, G = 8, G_sh = 9, A = 10, A_sh = 11, B = 12 }
        private byte octave;
        public byte Octave
        {
            get => octave;
            set
            {
                Validate.IsTrue(value >= 1 && value <= 9, "Выход за пределы диапазона октав.");
                octave = value;
            }
        }

        public static bool operator ==(Note note1, Note note2)
        {
            if (note1.CurNote == note2.CurNote && note1.Octave == note2.Octave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Note note1, Note note2)
        {
            if (note1.CurNote != note2.CurNote || note1.Octave != note2.Octave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Note()
        {
            CurNote = NoteName.C;
            Octave = 1;
        }

        public Note(NoteName note, byte octave)
        {
            CurNote = note;
            Octave = octave;
        }

        public Note(Note note)
        {
            CurNote = note.CurNote;
            Octave = note.Octave;
        }

        public void NextNote()
        {
            CurNote++;
            if (CurNote > NoteName.B)
            {
                CurNote = NoteName.C;
                Octave++;
            }
        }

        public void PreviousNote()
        {
            CurNote--;
            if (CurNote < NoteName.C)
            {
                CurNote = NoteName.B;
                Octave--;
            }
        }

        public void OffsetNote(int offset, bool reverse = false)
        {
            for (int i = 0; i < offset; i++)
            {
                if (!reverse)
                {
                    NextNote();
                }
                else
                {
                    PreviousNote();
                }
            }
        }

        public static NoteName ByteToNoteName(byte value)
        {
            if (value == 1)
            {
                return NoteName.C;
            }
            else if (value == 2)
            {
                return NoteName.C_sh;
            }
            else if (value == 3)
            {
                return NoteName.D;
            }
            else if (value == 4)
            {
                return NoteName.D_sh;
            }
            else if (value == 5)
            {
                return NoteName.E;
            }
            else if (value == 6)
            {
                return NoteName.F;
            }
            else if (value == 7)
            {
                return NoteName.F_sh;
            }
            else if (value == 8)
            {
                return NoteName.G;
            }
            else if (value == 9)
            {
                return NoteName.G_sh;
            }
            else if (value == 10)
            {
                return NoteName.A;
            }
            else if (value == 11)
            {
                return NoteName.A_sh;
            }
            else if (value == 12)
            {
                return NoteName.B;
            }

            throw new NotImplementedException();
        }

        public static NoteName StringToNoteName(string str)
        {
            switch (str)
            {
                case "C":
                    return NoteName.C;
                case "C#":
                    return NoteName.C_sh;
                case "D":
                    return NoteName.D;
                case "D#":
                    return NoteName.D_sh;
                case "E":
                    return NoteName.E;
                case "F":
                    return NoteName.F;
                case "F#":
                    return NoteName.F_sh;
                case "G":
                    return NoteName.G;
                case "G#":
                    return NoteName.G_sh;
                case "A":
                    return NoteName.A;
                case "A#":
                    return NoteName.A_sh;
                case "B":
                    return NoteName.B;
            }
            throw new NotImplementedException("Такой ноты не существует.");
        }
    }
}
