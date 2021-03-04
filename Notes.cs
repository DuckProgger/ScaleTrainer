using System;

namespace Scale_Trainer
{
    internal class Notes
    {
        public NoteName Note { get; set; } = NoteName.C;
        public enum NoteName : byte { C = 1, C_sh = 2, D = 3, D_sh = 4, E = 5, F = 6, F_sh = 7, G = 8, G_sh = 9, A = 10, A_sh = 11, B = 12 }
        private byte octave;
        public byte Octave
        {
            get => octave;
            set
            {
                if (value >= 1 && value <= 9)
                {
                    octave = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public Notes()
        {
            Note = NoteName.C;
            Octave = 1;
        }

        public Notes(NoteName note, byte octave)
        {
            Note = note;
            Octave = octave;
        }

        public NoteName GetNext()
        {
            Note++;
            if (Note > NoteName.B)
            {
                Note = NoteName.C;
            }
            return Note;
        }

        public NoteName GetPrevious()
        {
            Note--;
            if (Note < NoteName.C)
            {
                Note = NoteName.B;
            }
            return Note;
        }

        public NoteName GetCurrNote()
        {
            return Note;
        }

        public void SetCurrNote(NoteName note)
        {
            this.Note = note;
        }

        public static NoteName ByteToNote(byte value)
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

        public static NoteName StringToNote(string str)
        {
            switch (str)
            {
                case "C":
                    return Notes.NoteName.C;
                case "C#":
                    return Notes.NoteName.C_sh;
                case "D":
                    return Notes.NoteName.D;
                case "D#":
                    return Notes.NoteName.D_sh;
                case "E":
                    return Notes.NoteName.E;
                case "F":
                    return Notes.NoteName.F;
                case "F#":
                    return Notes.NoteName.F_sh;
                case "G":
                    return Notes.NoteName.G;
                case "G#":
                    return Notes.NoteName.G_sh;
                case "A":
                    return Notes.NoteName.A;
                case "A#":
                    return Notes.NoteName.A_sh;
                case "B":
                    return Notes.NoteName.B;
            }
            throw new NotImplementedException();
        }
    }
}
