using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Iterators
{
    public class Creature : IEnumerable<int>
    {
        private readonly int[] stats = new int[3];

        private const int strenght = 0;
        private const int agility = 1;
        private const int intelligence = 2;

        public int Strength { get => stats[strenght]; set => stats[strenght] = value; }
        public int Agility { get => stats[agility]; set => stats[agility] = value; }
        public int Intelligence { get => stats[intelligence]; set => stats[intelligence] = value; }
        public int this[int index] { get => stats[index]; set => stats[index] = value; }

        public double AverageStat => stats.Average();

        public IEnumerator<int> GetEnumerator() => stats.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class ArrayBackedProperties
    {
        public static void Start()
        {
            var creature = new Creature
            {
                Agility = 10,
                Intelligence = 30,
                Strength = 14 
            };

            System.Console.WriteLine(creature.AverageStat);
        }
    }
}
