using System;

namespace Patterns.Factories
{
    public class Exercise
    {
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Person(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString()
            {
                return $"{{{nameof(Id)}={Id}, {nameof(Name)}={Name}}}";
            }
        }

        public class PersonFactory
        {
            private int _personCount = 0;

            public Person CreatePerson(string name) => new Person(_personCount++, name);
        }

        public static void Start()
        {
            var factory = new PersonFactory();

            for (int index = 0; index < 5; index++)
            {
                var person = factory.CreatePerson($"Person {index}");
                Console.WriteLine(person);
            }
        }
    }
}
