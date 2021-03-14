namespace Scale_Trainer
{
    internal sealed class Guitar : StringedConfig
    {
        public Guitar(int strings, int frets, string tuningName) : base(strings, frets)
        {
            Tuning = new Tuning(this, tuningName);
        }
    }
}
