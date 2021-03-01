using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Notes
    {
        private int currNote = 0;
        public enum Note { C = 1, C_sh, D, D_sh, E, F, F_sh, G, G_sh, A, A_sh, B }

        public int GetNext()
        {
            if (currNote >= 12)
            {
                currNote = 0;
            }
            currNote++;
            return currNote;
        }

        public int GetPrevious()
        {
            currNote--;
            if (currNote <= 1)
            {
                currNote = 12;
            }
            return currNote;
        }

        public int GetCurrNote()
        {
            return currNote;
        }

        public void SetCurrNote(int note)
        {
            currNote = note;
        }
    }
}
