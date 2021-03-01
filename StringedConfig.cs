using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class StringedConfig
    {
        public int Strings { get; private set; }	// Количество струн
        public int Frets { get; private set; } // Количество ладов
        //public Tuning Tuning { get; private set; } // Строй

        public StringedConfig(int strings, int frets)
        {
            Strings = strings;
            Frets = frets;
            //Tuning = new Tuning(tuning);
        }
    }
}
