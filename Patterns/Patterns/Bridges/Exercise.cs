namespace Patterns.Bridges.Exercise
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs { get; }

        public VectorRenderer(string whatToRenderAs)
        {
            WhatToRenderAs = whatToRenderAs;
        }

        public override string ToString() => $"Drawing {WhatToRenderAs} as lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs { get; }

        public RasterRenderer(string whatToRenderAs)
        {
            WhatToRenderAs = whatToRenderAs;
        }

        public override string ToString() => $"Drawing {WhatToRenderAs} as pixels";
    }

    public abstract class Shape
    {
        protected readonly IRenderer _renderer;

        public string Name { get; protected set; }

        protected Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            
        }
    }

    public class VectorSquare : Square
    {
        public VectorSquare() : base(new VectorRenderer("Square"))
        {

        }

        public override string ToString() => _renderer.ToString();
    }

    public class RasterSquare : Square
    {
        public RasterSquare() : base(new RasterRenderer("Square"))
        {

        }

        public override string ToString() => _renderer.ToString();
    }

    internal class Exercise
    {
        internal static void Start()
        {
            var square = new VectorSquare();
            System.Console.WriteLine(square);
        }
    }
}
