﻿namespace Patterns.SOLID
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set => base.Width = base.Height = value;
        }
        public override int Height
        {
            set => base.Width = base.Height = value;
        }

        public Square()
        {

        }

        public Square(int size)
        {
            Width = size;
        }
    }
}
