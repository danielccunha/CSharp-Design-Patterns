using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns.Compositions
{
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color { get; set; }

        private Lazy<List<GraphicObject>> _children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => _children.Value;

        private void Print(StringBuilder builder, int depth)
        {
            builder.Append(new string('*', depth))
                   .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $" {Color}")
                   .AppendLine($" {Name}");

            foreach (var child in Children)
                child.Print(builder, depth + 4);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            Print(builder, 0);

            return builder.ToString();
        }
    }

    public class Circle : GraphicObject
    {
        public override string Name => nameof(Circle);
    }

    public class Square : GraphicObject
    {
        public override string Name => nameof(Square);
    }

    internal class Example
    {
        internal static void Start()
        {
            var drawing = new GraphicObject { Name = "My Drawing" };
            drawing.Children.Add(new Square { Color = "Red" });
            drawing.Children.Add(new Circle { Color = "Yellow" });

            var group = new GraphicObject();
            group.Children.Add(new Circle { Color = "Blue" });
            group.Children.Add(new Square { Color = "Blue" }); 
            drawing.Children.Add(group);

            Console.WriteLine(drawing);
        }
    }
}
