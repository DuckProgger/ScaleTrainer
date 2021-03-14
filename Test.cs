using System;
using System.Diagnostics;
namespace Scale_Trainer
{
    internal class Test
    {
        public static void test()
        {
            Guitar guitar = new Guitar(6, 24, "D");
            Scale scale = new Scale("Mohanangi", Note.NoteName.D_sh);
            StringedVisualisation giutarVis = new StringedVisualisation(guitar, scale);

            ShowNeck(giutarVis.AvailableFrets);
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
                    if(ActiveFrets[@string, fret])
                    {
                        if (fret < 10)
                        {
                            symbol = "   ";
                        }
                        else
                        {
                            symbol = "  ";
                        }
                        symbol +=fret.ToString();
                    }
                    else
                    {
                        symbol = "   |";
                    }
                    Console.Write(symbol);
                }
            }
        }
    }
}
