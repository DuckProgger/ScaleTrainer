using System;
using System.Diagnostics;
namespace Scale_Trainer
{
    internal class Test
    {
        public static void test()
        {
            Guitar guitar = new Guitar(6, 24, "Standard");
            Scale scale = new Scale("Major", Note.NoteName.D);
            StringedVisualisation giutarVis = new StringedVisualisation(guitar, scale);

            ShowNeck(giutarVis.AvailableFrets);
            CalcKoeff(1.0);
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
                    if (ActiveFrets[@string, fret])
                    {
                        if (fret < 10)
                        {
                            symbol = "   ";
                        }
                        else
                        {
                            symbol = "  ";
                        }
                        symbol += fret.ToString();
                    }
                    else
                    {
                        symbol = "   |";
                    }
                    Console.Write(symbol);
                }
            }
        }

        static double fret = 0;

        static void CalcKoeff(double value)
        {
            if (fret < 24)
            {
                double temp = value / Math.Pow(2, 0.083333);
                double k = (value - temp) * 133.333;
                fret++;
                Console.WriteLine("{0:#.###}", k);
                CalcKoeff(temp);
            }
            return;
        }

    }
}
