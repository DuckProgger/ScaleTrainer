using System;
using System.IO;

namespace Scale_Trainer
{
    internal class DataExchange
    {
        private enum TypeOfInstrument : byte { Guitar, Bass }

        public static Notes.NoteName[] GetTuningFromDB(StringedConfig instrument, Tuning.TuningName tuning)
        {
            
            byte[] tuningInfoByte;

            if (instrument is Guitar)
            {
                tuningInfoByte = GetTuningInfoFromDB((byte)TypeOfInstrument.Guitar, Tuning.EnumToByte(tuning));
            }
            else if (instrument is Bass)
            {
                tuningInfoByte = GetTuningInfoFromDB((byte)TypeOfInstrument.Bass, Tuning.EnumToByte(tuning));
            }
            else
            {
                throw new NotImplementedException();
            }

            Notes.NoteName[] tuningInfo = new Notes.NoteName[tuningInfoByte.Length];

            for (int i = 0; i < tuningInfo.Length; i++)
            {
                tuningInfo[i] = Notes.ByteToNote(tuningInfoByte[i]);
            }

            return tuningInfo; 
        }

        private static byte[] GetTuningInfoFromDB(byte instrument, byte tuning)
        {
            byte[] buffer = GetByteBufferFromDB(@"data/tuning.db");
            byte[] tuningInfoCom = new byte[14];
            byte[] tuningInfo = null;

            for (int i = 0; i < buffer.Length; i += 16)
            {
                if (buffer[i] == instrument && buffer[i + 1] == tuning)
                {
                    int bitNumber = 0;
                    for (int j = i + 2; j < 16; j++, bitNumber++)
                    {
                        if(buffer[j] == 0)
                        {
                            tuningInfo = new byte[bitNumber];
                            Array.Copy(tuningInfoCom, tuningInfo, bitNumber);
                            break;
                        }
                        tuningInfoCom[bitNumber] = buffer[j];
                    }
                    return tuningInfo;
                }
                else
                {
                    continue;
                }
            }

            throw new FileNotFoundException();

        }

        public static byte[] GetByteBufferFromDB(string path, int offset, int count)
        {
            byte[] buffer = new byte[count];
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                fs.Read(buffer, offset, count);
            }
            return buffer;
        }

        public static byte[] GetByteBufferFromDB(string path)
        {
            return File.ReadAllBytes(path);
        }


    }
}
