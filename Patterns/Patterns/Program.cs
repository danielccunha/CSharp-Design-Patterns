using Patterns.SOLID;
using System;

namespace Patterns
{
    class Program
    {
        static void Main()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Huge);

            var products = new Product[] { apple, tree, house };
            var betterFilter = new BetterFilter();

            Console.WriteLine("Huge blue products:");

            foreach (var product in betterFilter.Filter(products, new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Huge))))
                Console.WriteLine($"- {product.Name} is {product.Color}");
        }
    }
}
