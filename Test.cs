using System;
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

            while (!giutarVis.EndOfExercise)
            {
                Console.Clear();
                ShowNeck(giutarVis.ActiveFrets);
                giutarVis.SetFretsNextString();
            }




            ;

        }

        static void ShowNeck(bool[,] ActiveFrets)
        {
            int strings = ActiveFrets.GetLength(0);
            int frets = ActiveFrets.GetLength(1);
            string symbol;

            for (int @string = 0; @string < strings; @string++, Console.WriteLine())
            {
                for (int fret = 0; fret < frets; fret++)
                {
                    symbol = ActiveFrets[@string, fret] ? " " + fret.ToString() : " - ";
                    Console.Write(symbol);
                }
            }
        }
    }
}
