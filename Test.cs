using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scale_Trainer
{
    class Test
    {
        public static void test()
        {
            Guitar guitar = new Guitar(6, 24, Tuning.TuningName.Standard);
            //StringedVisualisation giutarVis = new StringedVisualisation(guitar);
            //byte[] buffer = DataExchange.GetByteBufferFromDB(@"data/tuning.db", 0, 7);
            ;
        }
    }
}
