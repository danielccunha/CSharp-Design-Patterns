using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Patterns.Prototypes
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);

                object copy = formatter.Deserialize(stream);
                return (T)copy;
            }   
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, self);

                stream.Position = 0;

                return (T)serializer.Deserialize(stream);
            }            
        }
    }

    [Serializable]
    public class Person
    {
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person()
        {

        }

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
    }

    [Serializable]
    public class Address
    {
        public string Street { get; set; }
        public int Number { get; set; }

        public Address()
        {

        }

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
    }

    public class PrototypesExample
    {
        public static void Start()
        {
            var john = new Person(new string[] { "John", "Smith" }, new Address("London Road", 1900));
            var jane = john.DeepCopyXml();

            jane.Names[0] = "Jane";
            jane.Address.Number = 2000;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
