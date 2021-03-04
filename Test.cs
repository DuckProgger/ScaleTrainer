namespace Scale_Trainer
{
    internal class Test
    {
        public static void test()
        {
            //Guitar guitar = new Guitar(6, 24, Tuning.TuningName.D);
            //StringedVisualisation giutarVis = new StringedVisualisation(guitar);
            //string[] str = DataExchange.GetScaleListFromXML();
            var test = DataExchange.TryFindScale("Major", out Scale sc);
            ;

        }
    }
}
