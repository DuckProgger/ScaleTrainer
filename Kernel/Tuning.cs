namespace Scale_Trainer
{
    internal sealed class Tuning
    {
        /// <summary>
        /// Ноты открытых струн инструмента.
        /// </summary>
        public Note[] Notes { get; private set; } 

        public Tuning(StringedInstrument instrument, string tuningName)
        {
            Notes = DataExchange.GetTuningFromXml(instrument, tuningName);
        }
    }
}
