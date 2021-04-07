using System;

namespace Scale_Trainer
{
    internal sealed class NameAttribute : Attribute
    {
        private readonly string name;
        public string Name => name;
        public NameAttribute(string name)
        {
            this.name = name;
        }
    }
}
