using System.Collections.Generic;
using System.Linq;

namespace Patterns.SOLID
{
    public enum Color
    {
        Red,
        Green,
        Blue
    }

    public enum Size
    {
        Small,
        Medium,
        Large,
        Huge
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var product in products)
                if (product.Size == size)
                    yield return product;
        }

        public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var product in products)
                if (product.Color == color)
                    yield return product;
        }

        public static IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var product in products)
                if (product.Size == size && product.Color == color)
                    yield return product;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == _color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == _size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _firstSpec;
        private readonly ISpecification<T> _secondSpec;

        public AndSpecification(ISpecification<T> firstSpec, ISpecification<T> secondSpec)
        {
            _firstSpec = firstSpec;
            _secondSpec = secondSpec;
        }

        public bool IsSatisfied(T t)
        {
            return _firstSpec.IsSatisfied(t) && _secondSpec.IsSatisfied(t);
        }
    }

    public class OrSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _firstSpec;
        private readonly ISpecification<T> _secondSpec;

        public OrSpecification(ISpecification<T> firstSpec, ISpecification<T> secondSpec)
        {
            _firstSpec = firstSpec;
            _secondSpec = secondSpec;
        }

        public bool IsSatisfied(T t)
        {
            return _firstSpec.IsSatisfied(t) || _secondSpec.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }
}
