using System;

namespace Scale_Trainer
{
    internal class Validate
    {
        public static void IsTrue(bool condition, string message = "")
        {
            if (!condition)
            {
                throw new Exception(message);
            }
        }
    }
}
