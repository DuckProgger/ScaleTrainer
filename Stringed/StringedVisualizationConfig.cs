namespace Scale_Trainer
{
    internal class StringedVisualizationConfig
    {
        /// <summary>
        /// Номер струны, с которой нужно начать гамму.
        /// </summary>
        public int FirstString { get; private set; } = -1;
        /// <summary>
        /// Номер лада, с которого нужно начать гамму.
        /// </summary>
        public int FirstFret { get; private set; } = -1;
        /// <summary>
        /// Количество одновременно отображаемых нот на грифе.
        /// </summary>
        public int NotesOnNeck { get; private set; } = 6;
        /// <summary>
        /// Количество одновременно отображаемых нот на струне.
        /// </summary>
        public int NotesOnString { get; private set; } = 3;
        /// <summary>
        /// Максимальное количество ладов между крайними нотами на одной струне.
        /// </summary>
        public int MaxInterval { get; private set; } = 4;

        

        public void SetFirstFret(int @string, int fret)
        {
            FirstString = @string;
            FirstFret = fret;
        }

        public void SetNotesOnNeck(int newValue)
        {
            NotesOnNeck = newValue;
        }

        public void SetNotesOnString(int newValue)
        {
            NotesOnString = newValue;
        }

        public void SetMaxInterval(int newValue)
        {
            MaxInterval = newValue;
        }





    }
}
