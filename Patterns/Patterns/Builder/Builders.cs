using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns.Builder
{
    public static class Builders
    {
        public class HtmlElement
        {
            private const int indentSize = 2;

            public string Name, Text;
            public IList<HtmlElement> Elements = new List<HtmlElement>();

            public HtmlElement()
            {

            }

            public HtmlElement(string name, string text)
            {
                Name = name;
                Text = text;
            }


            private string ToStringImpl(int indent)
            {
                var builder = new StringBuilder();
                var indentStr = new string(' ', indent * indentSize);

                builder.AppendLine($"{indentStr}<{Name}>");

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    builder.Append($"{indentStr}{indentStr}");
                    builder.AppendLine(Text);
                }

                foreach (var element in Elements)
                    builder.Append(element.ToStringImpl(indent + 1));

                builder.AppendLine($"{indentStr}</{Name}>");

                return builder.ToString();
            }

            public override string ToString() => ToStringImpl(0);
        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            private HtmlElement root = new HtmlElement();

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            public HtmlBuilder AddChild(string childName, string childText)
            {
                var element = new HtmlElement(childName, childText);
                root.Elements.Add(element);

                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement { Name = rootName };
            }
        }

        public static void Start()
        {
            var builder = new HtmlBuilder("ul");

            builder
                .AddChild("li", "Lorem ipsum")
                .AddChild("li", "Dolor sit amet");

            Console.WriteLine(builder);
        }
    }
}
