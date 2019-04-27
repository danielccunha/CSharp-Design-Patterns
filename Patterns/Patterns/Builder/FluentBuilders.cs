using System;

namespace Patterns.Builder
{
    public class FluentBuilders
    {
        public class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }

            public class Builder : PersonJobBuilder<Builder>
            {

            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        public abstract class PersonBuilder
        {
            protected Person Person { get; } = new Person();

            public Person Build() => Person;
        }

        public class PersonInfoBuilder<T> : PersonBuilder where T : PersonInfoBuilder<T>
        {
            public T Called(string name)
            {
                Person.Name = name;
                return (T)this;
            }
        }

        public class PersonJobBuilder<T> : PersonInfoBuilder<T> where T : PersonJobBuilder<T>
        {
            public T WorkAsA(string job)
            {
                Person.Position = job;
                return (T) this;
            }
        }

        internal static void Start()
        {
            var me = Person.New
                .Called("Daniel")
                .WorkAsA("Developer")
                .Build();

            Console.WriteLine(me);
        }
    }
}
