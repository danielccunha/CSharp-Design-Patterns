using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Patterns.Prototypes
{
    public class Exercise
    {
        [Serializable]
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"{{{nameof(X)}={X}, {nameof(Y)}={Y}}}";
            }
        }

        [Serializable]
        public class Line
        {
            public Point Start { get; set; }
            public Point End { get; set; }

            public Line DeepCopy()
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();

                    formatter.Serialize(stream, this);
                    stream.Seek(0, SeekOrigin.Begin);

                    object copy = formatter.Deserialize(stream);
                    return (Line)copy;
                }
            }

            public override string ToString()
            {
                return $"{{{nameof(Start)}={Start}, {nameof(End)}={End}}}";
            }
        }

        public static void Start()
        {
            var line = new Line
            {
                Start = new Point(0, 0),
                End = new Point(50, 50)
            };
            var copiedLine = line.DeepCopy();

            copiedLine.Start = new Point(10, 10);

            Console.WriteLine(line);
            Console.WriteLine(copiedLine);
        }
    }
}
