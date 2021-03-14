﻿namespace Scale_Trainer
{
    internal sealed class Tuning
    {
        /// <summary>
        /// Ноты открытых струн инструмента.
        /// </summary>
        public Note[] Notes { get; private set; } 

        public Tuning(StringedConfig instrument, string tuningName)
        {
            Notes = DataExchange.GetTuningFromXML(instrument, tuningName);
        }
    }
}
