using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Bass : StringedConfig
    {
        public Bass(int strings, int frets, Tuning.TuningName tuning) : base(strings, frets, tuning) { }
    }
}
