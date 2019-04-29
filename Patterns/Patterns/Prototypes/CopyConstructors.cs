using System;

namespace Patterns.Prototypes
{
    public class CopyConstructors
    {
        public interface IPrototype<T>
        {
            T DeepCopy();
        }

        public class Person : IPrototype<Person>
        {
            public string[] Names { get; set; }
            public Address Address { get; set; }

            public Person(string[] names, Address address)
            {
                Names = names;
                Address = address;
            }

            public Person(Person other)
            {
                if (other == null)                
                    throw new ArgumentNullException(nameof(other));

                Names = other.Names;
                Address = new Address(other.Address);
            }

            public override string ToString()
            {
                return $"{{ {nameof(Names)}={string.Join(" ", Names)}, {nameof(Address)}={Address} }}";
            }

            public Person DeepCopy()
            {
                return new Person(Names, Address.DeepCopy());
            }
        }

        public class Address : IPrototype<Address>
        {
            public string Street { get; set; }
            public int Number { get; set; }

            public Address(string street, int number)
            {
                Street = street;
                Number = number;
            }

            public Address(Address other)
            {
                Street = other.Street;
                Number = other.Number;
            }

            public override string ToString()
            {
                return $"{{ {nameof(Street)}={Street}, {nameof(Number)}={Number} }}";
            }

            public Address DeepCopy()
            {
                return new Address(Street, Number);
            }
        }

        public static void Start()
        {
            var john = new Person(new string[] { "John", "Smith" }, new Address("London Road", 1900));
            var jane = new Person(john);

            jane.Names[0] = "Jane";
            jane.Address.Number = 1000;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
