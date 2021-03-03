using System;

namespace Scale_Trainer
{
    internal class Tuning
    {
        public Notes.NoteName[] NamesOfNotes { get; private set; }
        public enum TuningName : byte { Standard, D, D_drop }

        public Tuning(StringedConfig instrument, TuningName tuning)
        {
            NamesOfNotes = DataExchange.GetTuningFromDB(instrument, tuning);
        }

        public static byte EnumToByte(TuningName en)
        {
            if (en == TuningName.Standard)
            {
                return (byte)TuningName.Standard;
            }
            else if (en == TuningName.D)
            {
                return (byte)TuningName.D;
            }
            else if (en == TuningName.D_drop)
            {
                return (byte)TuningName.D_drop;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
