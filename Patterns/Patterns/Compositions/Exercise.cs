using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Compositions.Exercise
{
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value { get; set; }

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;

            foreach (var container in containers)
                result += container.Sum();

            return result;
        }
    }

    internal class Exercise
    {
        internal static void Start()
        {

        }
    }
}
