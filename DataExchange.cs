using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale_Trainer
{
    class DataExchange
    {
        public static Notes.NoteName[] GetTuningFromDB(StringedConfig instrument, Tuning.TuningName tuning)
        {
            if (instrument is Guitar)
            {
                //
            }
            else if (instrument is Bass)
            {
                //
            }
            else
            {
                throw new NotImplementedException();
            }


            return new Notes.NoteName[0]; // заглушка
        }

        //private byte[] GetByteBufferfromDB(string path, int offset, int count)
        //{

        //}


    }
}
