using System;

namespace Scale_Trainer
{
    internal static class Validate
    {
        // если условие не срабатывает, то вызывается исключение
        public static void IsTrue(bool condition, string message = "")
        {
            if (!condition)
            {
                throw new Exception(message);
            }
        }

        // если условие срабатывает, то вызывается исключение
        public static void IsFalse(bool condition, string message = "")
        {
            if (condition)
            {
                throw new Exception(message);
            }
        }
    }
}
