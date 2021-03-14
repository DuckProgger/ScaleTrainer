namespace Scale_Trainer
{
    internal sealed class Bass : StringedConfig
    {
        public Bass(int strings, int frets, string tuning) : base(strings, frets)
        {
            Tuning = new Tuning(this, tuning);
        }
    }
}
