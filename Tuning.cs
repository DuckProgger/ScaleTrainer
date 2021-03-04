namespace Scale_Trainer
{
    internal sealed class Tuning
    {
        public Note[] Notes { get; private set; }
        public enum TuningName : byte { Standard, D, D_drop }

        public Tuning(StringedConfig instrument, TuningName tuning)
        {
            Notes = DataExchange.GetTuningFromXML(instrument, tuning);
        }
    }
}
