namespace Scale_Trainer
{
    internal abstract class StringedConfig : ITunable
    {
        /// <summary>
        /// Количество струн струнного инструмента.
        /// </summary>
        public int Strings { get; private set; }	
        /// <summary>
        /// Количество ладов струнного инструмента.
        /// </summary>
        public int Frets { get; private set; } 
        /// <summary>
        /// Строй струнного инструмента.
        /// </summary>
        public Tuning Tuning { get; set; } 

        protected StringedConfig(int strings, int frets)
        {
            Strings = strings;
            Frets = frets;
        }
    }
}
