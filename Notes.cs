using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Notes
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
    }
}
