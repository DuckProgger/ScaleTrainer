using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Guitar : StringedConfig, ITunable
    {
        public Tuning Tuning { get; set; } // Строй

        public Guitar(int strings, int frets, Tuning.TuningName tuning) : base(strings, frets) 
        {
            Tuning = new Tuning(this, tuning);
        }
    }
}
