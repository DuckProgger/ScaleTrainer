namespace Scale_Trainer
{
    internal class StringedVisualizationConfig
    {
        public int FirstString { get; private set; }

        public StringedVisualizationConfig()
        {
            FirstString = 6;
        }

        public StringedVisualizationConfig(int firstString)
        {
            FirstString = firstString;
        }

    }
}
