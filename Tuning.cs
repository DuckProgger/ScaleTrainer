namespace Scale_Trainer
{
    internal class Tuning
    {
        public Notes.NoteName[] NamesOfNotes { get; private set; }
        public enum TuningName { Standard, D, D_drop };

        public Tuning(StringedConfig instrument, TuningName tuning)
        {
            NamesOfNotes = DataExchange.GetTuningFromDB(instrument, tuning);
        }
    }
}
