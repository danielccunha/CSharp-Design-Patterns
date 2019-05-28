namespace Patterns.Adapters
{
    public class Square
    {
        public int Side { get; }

        public Square(int side)
        {
            Side = side;
        }
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public SquareToRectangleAdapter(Square square)
        {
            Width = Height = square.Side;
        }

        public int Width { get; }
        public int Height { get; }
    }

    public class Exercise
    {
        public static void Start()
        {

        }
    }
}
