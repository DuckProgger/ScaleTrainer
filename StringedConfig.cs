namespace Scale_Trainer
{
    internal abstract class StringedConfig : ITunable
    {
        public int Strings { get; private set; }	// Количество струн
        public int Frets { get; private set; } // Количество ладов
        public Tuning Tuning { get; set; } // Строй

        protected StringedConfig(int strings, int frets)
        {
            Strings = strings;
            Frets = frets;
        }
    }
}
