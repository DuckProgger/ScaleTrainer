﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class Test
    {
        public static void test()
        {
            Stringed guitar = new Stringed(6, 24, Tuning.TuningName.Standard);
            StringedVisualisation giutarVis = new StringedVisualisation(guitar);
            ;
        }
    }
}
