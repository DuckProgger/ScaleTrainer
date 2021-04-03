namespace Scale_Trainer
{
    internal sealed class Bass : StringedInstrument
    {
        public Bass(int strings, int frets, string tuning) : base(strings, frets)
        {
            Tuning = new Tuning(this, tuning);
        }
    }
}
