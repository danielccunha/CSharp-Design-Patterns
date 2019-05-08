using Autofac;
using System;

namespace Patterns.Bridges
{
    internal interface IRenderer
    {
        void RenderCircle(float radius);
    }

    internal class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}.");
        }
    }

    internal class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle with radius {radius}.");
        }
    }

    internal abstract class Shape
    {
        protected readonly IRenderer _renderer;

        protected Shape(IRenderer renderer)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    internal class Circle : Shape
    {
        private float _radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            _renderer.RenderCircle(_radius);
        }

        public override void Resize(float factor)
        {
            _radius *= factor;
        }
    }

    internal class Example
    {
        internal static void Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
            builder.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));

            using (var container = builder.Build())
            {
                var circle = container.Resolve<Circle>(new PositionalParameter(0, 10f));

                circle.Draw();
                circle.Resize(3);
                circle.Draw();
            }
        }
    }
}
