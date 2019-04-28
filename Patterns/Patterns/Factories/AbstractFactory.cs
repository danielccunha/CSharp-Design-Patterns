using System;
using System.Collections.Generic;

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
        public enum AvailableDrink
        {
            Coffee, Tea
        }

        private readonly Dictionary<AvailableDrink, IHotDrinkFactory> _factories 
            = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory)Activator.CreateInstance(
                    Type.GetType($"Patterns.Factories.{Enum.GetName(typeof(AvailableDrink), drink)}Factory"));

                _factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount) => _factories[drink].Prepare(amount);
    }

    internal class AbstractFactory
    {
        internal static void Start()
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);

            drink.Consume();
        }
    }
}
