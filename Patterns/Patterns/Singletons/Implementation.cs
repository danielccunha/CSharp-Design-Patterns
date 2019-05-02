using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Patterns.Singletons
{
    internal interface IDatabase
    {
        int GetPopulation(string name);
    }

    internal class SingletonDatabase : IDatabase
    {
        private readonly Dictionary<string, int> _capitals;
        private static readonly Lazy<SingletonDatabase> _instance 
            = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => _instance.Value;

        private SingletonDatabase()
        {
            _capitals = File.ReadAllLines(@"E:\Github\CSharp-Design-Patterns\Patterns\Patterns\Singletons\Capitals.txt")
                .Batch(2)
                .ToDictionary(list => list.ElementAt(0).Trim(),
                              list => int.Parse(list.ElementAt(1)));
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }

    internal class Implementation
    {
        internal static void Start()
        {
            var city = "Sao Paulo";
            var population = SingletonDatabase.Instance.GetPopulation(city);

            Console.WriteLine($"{city} has population {population}");
        }
    }
}
