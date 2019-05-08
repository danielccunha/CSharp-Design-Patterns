using System;

namespace Patterns.Singletons
{
    internal interface ISingleton
    {
        object Object { get; }
    }

    internal class Singleton : ISingleton
    {
        private static readonly Lazy<Singleton> _instance
            = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => _instance.Value;

        private Singleton()
        {

        }

        public object Object { get; } = new object();
    }

    internal class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            var obj1 = func.Invoke();
            var obj2 = func.Invoke();

            return obj1.Equals(obj2);
        }
    }

    internal class Exercise
    {
        internal static void Start()
        {
            var isSingleton = SingletonTester.IsSingleton(() => Singleton.Instance);
            Console.WriteLine(isSingleton);
        }
    }
}
