using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Patterns.Builder
{
    internal class Exercise
    {
        public class ClassElement
        {
            private const int IndentSize = 4;

            public Modifier Modifier { get; }
            public string Name { get; }
            public IList<ClassProperty> Properties { get; } = new List<ClassProperty>();

            public ClassElement(string name, Modifier modifier = Modifier.@public)
            {
                Name = name;
                Modifier = modifier;
            }

            public override string ToString()
            {
                var builder = new StringBuilder();
                var indentStr = new string(' ', IndentSize);

                builder.AppendLine($"{Modifier} class {Name}");
                builder.AppendLine("{");

                var properties = string.Join('\n', Properties.Select(p => $"{indentStr}{p}"));
                builder.AppendLine(properties);
                builder.AppendLine("}");

                return builder.ToString();
            }
        }

        public class ClassProperty
        {
            public Modifier Modifier { get; }
            public string Type { get; }
            public string Name { get; }

            public ClassProperty(string type, string name, Modifier modifier = Modifier.@public)
            {
                Modifier = modifier;
                Type = type;
                Name = name;
            }

            public override string ToString()
            {
                return $"{Modifier} {Type} {Name} {{ get; set; }}";
            }
        }

        public class ClassBuilder
        {
            private ClassElement Class { get; }

            public ClassBuilder(string className, Modifier modifier = Modifier.@public)
            {
                Class = new ClassElement(className, modifier);
            }

            public ClassBuilder AddProperty(string type, string name, Modifier modifier = Modifier.@public)
            {
                var property = new ClassProperty(type, name, modifier);
                Class.Properties.Add(property);

                return this;
            }

            public ClassElement Build() => Class;
        }

        public enum Modifier
        {
            [Description("pubic")]
            @public,

            [Description("private")]
            @private,

            [Description("internal")]
            @internal,

            [Description("protected")]
            @protected
        }

        internal static void Start()
        {
            var builder = new ClassBuilder("City");

            var @class = builder
                .AddProperty("string", "Name")
                .AddProperty("string", "State")
                .AddProperty("int", "Code")
                .Build();

            Console.WriteLine(@class);
        }
    }
}
