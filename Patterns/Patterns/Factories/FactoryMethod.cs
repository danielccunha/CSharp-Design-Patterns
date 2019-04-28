using System;

namespace Patterns.Factories
{
    public class Point
    {
        private double X { get; }
        private double Y { get; }

        private Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{{{X}, {Y}}}";
        }

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y) => new Point(x, y);

            public static Point NewPolarPoint(double rho, double theta) => new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    internal class FactoryMethod
    {
        internal static void Start()
        {
            var point = Point.Factory.NewCartesianPoint(0, 0);
            Console.WriteLine(point);
        }
    }
}
