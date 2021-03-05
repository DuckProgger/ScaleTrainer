namespace Scale_Trainer
{
    internal class StringedVisualizationConfig
    {
        /// <summary>
        /// Номер струны, с которой нужно начать гамму.
        /// </summary>
        public int FirstString { get; private set; } 
        /// <summary>
        /// Количество одновременно отображаемых нот на грифе.
        /// </summary>
        public int NotesOnNeck { get; private set; }
        /// <summary>
        /// Максимальное количество ладов между крайними нотами на одной струне.
        /// </summary>
        public int MaxInterval { get; private set; }


        public StringedVisualizationConfig()
        {
            FirstString = 6;
            NotesOnNeck = 6;
            MaxInterval = 4;
        }

        //public StringedVisualizationConfig(int firstString)
        //{
        //    FirstString = firstString;
        //}

        public void SetFirstString(int newValue)
        {
            FirstString = newValue;
        }

        public void SetNotesOnNeck(int newValue)
        {
            NotesOnNeck = newValue;
        }

        public void SetMaxInterval(int newValue)
        {
            MaxInterval = newValue;
        }





    }
}
