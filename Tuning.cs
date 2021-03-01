using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Tuning
    {
        public Notes.NoteName[] NameNotes { get; private set; }
        public enum TuningName { Standard, D, D_drop };

        public Tuning(TuningName tuning)
        {
            switch (tuning)
            {
                case TuningName.Standard:
                    NameNotes = new Notes.NoteName[]
                    {
                        Notes.NoteName.E,
                        Notes.NoteName.A,
                        Notes.NoteName.D,
                        Notes.NoteName.G,
                        Notes.NoteName.B,
                        Notes.NoteName.E
                    };
                    break;
            }
        }
    }

}
