using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Stringed
    {
        public int Strings { get; private set; }	// Количество струн
        public int Frets { get; private set; } // Количество ладов
        public Tuning Tuning { get; private set; } // Строй

        public Stringed(int strings, int frets, Tuning.TuningName tuning)
        {
            Strings = strings;
            Frets = frets;
            Tuning = new Tuning(tuning);
        }
    }
}
