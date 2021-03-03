using System;

namespace Scale_Trainer
{
    internal class Notes
    {
        private NoteName currNote = NoteName.C;
        public enum NoteName { C = 1, C_sh, D, D_sh, E, F, F_sh, G, G_sh, A, A_sh, B }

        public NoteName GetNext()
        {
            currNote++;
            if (currNote > NoteName.B)
            {
                currNote = NoteName.C;
            }
            return currNote;
        }

        public NoteName GetPrevious()
        {
            currNote--;
            if (currNote < NoteName.C)
            {
                currNote = NoteName.B;
            }
            return currNote;
        }

        public NoteName GetCurrNote()
        {
            return currNote;
        }

        public void SetCurrNote(NoteName note)
        {
            currNote = note;
        }

        public static NoteName ByteToNote(byte value)
        {
            if(value == 1)
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
    }
}
