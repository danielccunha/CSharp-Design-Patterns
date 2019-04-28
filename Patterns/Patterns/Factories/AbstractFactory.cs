using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Factories
{
    public interface IHotDrink
    {
        void Consume();
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Drinking Tea...");
        }
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            return new Tea();
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Drinking coffee...");
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        private readonly Dictionary<string, IHotDrinkFactory> factories 
            = new Dictionary<string, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (var type in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(type) && !type.IsInterface)
                {
                    var name = type.Name.Replace("Factory", string.Empty);
                    var factory = (IHotDrinkFactory)Activator.CreateInstance(type);

                    factories.Add(name, factory);
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks:");

            foreach (var drink in factories.Keys)            
                Console.WriteLine($"- {drink}");

            while (true)
            {
                Console.Write("\nChosen drink: ");
                var input = Console.ReadLine();
                var pair = factories.FirstOrDefault(f => f.Key.ToLower() == (input?.ToLower() ?? string.Empty));

                if (pair.Value != null)
                {
                    Console.Write("Amout: ");
                    input = Console.ReadLine();

                    if (input != null && int.TryParse(input, out int amount))
                        return pair.Value.Prepare(amount);
                }

                Console.WriteLine("Invalid input. Try again.");
            }
        }
    }

    internal class AbstractFactory
    {
        internal static void Start()
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();

            drink.Consume();
        }
    }
}
