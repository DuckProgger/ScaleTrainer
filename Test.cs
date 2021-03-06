namespace Scale_Trainer
{
    internal class Test
    {
        public static void test()
        {
            Guitar guitar = new Guitar(6, 24, Tuning.TuningName.D);
            StringedVisualizationConfig config = new StringedVisualizationConfig();
            Scale scale = new Scale("Major", Note.NoteName.C);

            StringedVisualisation giutarVis = new StringedVisualisation(guitar, config, scale);

            giutarVis.SetFretsNextString();
            giutarVis.SetFretsNextString();
            giutarVis.SetFretsNextString();

            giutarVis.SetFretsNextString();
            giutarVis.SetFretsNextString();


            ;

        }
    }
}
