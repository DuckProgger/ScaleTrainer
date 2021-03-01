using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Tuning
    {
        public int[] Notes { get; private set; }
        public enum Name { Standard, D, D_drop};

        public Tuning(Name name)
        {
            switch (name)
            {
                case Name.Standard:
                    Notes = new int[]
                    {
                        (int)Scale_Trainer.Notes.Note.E,
                        (int)Scale_Trainer.Notes.Note.A,
                        (int)Scale_Trainer.Notes.Note.D,
                        (int)Scale_Trainer.Notes.Note.G,
                        (int)Scale_Trainer.Notes.Note.B,
                        (int)Scale_Trainer.Notes.Note.E
                    };
                    break;
            }
        }
    }

}
