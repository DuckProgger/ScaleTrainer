﻿namespace Scale_Trainer
{
    [Name("Гитара")]
    internal sealed class Guitar : StringedInstrument
    {
        public Guitar(int strings, int frets, string tuningName) : base(strings, frets)
        {
            Tuning = new Tuning(this, tuningName);
        }
    }
}
