using Patterns.SOLID;
using System;

namespace Patterns
{
    class Program
    {
        public static int Area(Rectangle rectangle) => rectangle.Height * rectangle.Width;

        static void Main()
        {
            var rectangle = new Rectangle(10, 20);
            Console.WriteLine($"{rectangle} has area {Area(rectangle)}");

            var square = new Square(20);
            Console.WriteLine($"{square} has area {Area(square)}");
        }
    }
}
