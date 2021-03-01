using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Tuning
    {
        public Notes.NoteName[] NamesOfNotes { get; private set; }
        public enum TuningName { Standard, D, D_drop };       

        public Tuning(StringedConfig instrument, TuningName tuning)
        {
            NamesOfNotes = DataExchange.GetTuningFromDB(instrument, tuning);
        }   
    }
}
