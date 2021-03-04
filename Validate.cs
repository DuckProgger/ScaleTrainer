using System;

namespace Scale_Trainer
{
    internal static class Validate
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
