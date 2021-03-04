using System.IO;

namespace Scale_Trainer
{
    internal class Scale
    {
        public string Name { get; private set; }
        public int[] Intervals { get; private set;}

        public Scale(string name, int[] intervals)
        {           
            Name = name;
            Intervals = intervals;
        }

        public Scale(string name)
        {
            if (DataExchange.TryFindScale(name, out Scale newScale))
            {
                Name = newScale.Name;
                Intervals = newScale.Intervals;
            }
            else
            {
                throw new FileNotFoundException("Гамма не найдена.");
            }
        }
    }
}
