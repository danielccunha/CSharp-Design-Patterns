using System;

namespace Patterns.Builder
{
    public class FacetedBuilders
    {
        public class Person
        {
            // Address
            public string Address { get; set; }
            public string Postcode { get; set; }
            public string City { get; set; }

            // Employment
            public string Company { get; set; }
            public string Position { get; set; }
            public double AnnualIncome { get; set; }

            public override string ToString()
            {
                return $"{{{nameof(Address)}={Address}, {nameof(Postcode)}={Postcode}, {nameof(City)}={City}, {nameof(Company)}={Company}, {nameof(Position)}={Position}, {nameof(AnnualIncome)}={AnnualIncome}}}";
            }
        }

        public class PersonBuilder
        {
            protected Person Person = new Person();

            public PersonAddressBuilder Address => new PersonAddressBuilder(Person);
            public PersonJobBuilder Works => new PersonJobBuilder(Person);

            public static implicit operator Person(PersonBuilder builder) => builder.Person;

            public Person Build() => Person;
        }

        public class PersonAddressBuilder : PersonBuilder
        {
            public PersonAddressBuilder(Person person)
            {
                Person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                Person.Address = streetAddress;
                return this;
            }

            public PersonAddressBuilder WithPostcode(string postcode)
            {
                Person.Postcode = postcode;
                return this;
            }

            public PersonAddressBuilder In(string city)
            {
                Person.City = city;
                return this;
            }
        }

        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person)
            {
                Person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                Person.Company = companyName;
                return this;
            }

            public PersonJobBuilder AsA(string position)
            {
                Person.Position = position;
                return this;
            }

            public PersonJobBuilder Earning(double annualIncome)
            {
                Person.AnnualIncome = annualIncome;
                return this;
            }
        }

        internal static void Start()
        {
            var builder = new PersonBuilder();

            var person = builder
                .Address.At("Rua Independência")
                        .WithPostcode("00000-000")
                        .In("Passo Fundo")
                .Works.At("Ren9ve")
                      .AsA("Developer")
                      .Earning(100000)
                .Build();

            Console.WriteLine(person);
        }
    }
}
