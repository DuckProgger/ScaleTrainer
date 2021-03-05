namespace Scale_Trainer
{
    internal class Test
    {
        public static void test()
        {
            Guitar guitar = new Guitar(6, 24, Tuning.TuningName.D);
            StringedVisualizationConfig config = new StringedVisualizationConfig(5);
            StringedVisualisation giutarVis = new StringedVisualisation(guitar, config);
            //string[] str = DataExchange.GetScaleListFromXML();
            Scale sc = new Scale("Major", Note.NoteName.C);
            giutarVis.SetFirstCoord(sc.Key);
            ;

        }
    }
}
