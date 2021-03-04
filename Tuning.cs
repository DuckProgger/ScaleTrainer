namespace Scale_Trainer
{
    internal sealed class Tuning
    {
        public Notes[] NamesOfNotes { get; private set; }
        public enum TuningName : byte { Standard, D, D_drop }

        public Tuning(StringedConfig instrument, TuningName tuning)
        {
            NamesOfNotes = DataExchange.GetTuningFromXML(instrument, tuning);
        }
    }
}
