namespace Scale_Trainer
{
    internal sealed class Guitar : StringedConfig
    {
        public Guitar(int strings, int frets, Tuning.TuningName tuning) : base(strings, frets)
        {
            Tuning = new Tuning(this, tuning);
        }
    }
}
