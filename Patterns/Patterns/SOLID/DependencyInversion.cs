using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.SOLID
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name { get; set; }

        public Person()
        {

        }

        public Person(string name)
        {
            Name = name;
        }
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // Low-level
    public class Relationships : IRelationshipBrowser
    {
        private readonly List<(Person, Relationship, Person)> _relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return _relations
                .Where(r => r.Item1.Name == name && r.Item2 == Relationship.Parent)
                .Select(r => r.Item3);
        }
    }

    public class Research
    {
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
                Console.WriteLine($"John has a child called {p.Name}");
        }

        public static void Start()
        {
            var parent = new Person("John");
            var childOne = new Person("Chris");
            var childTwo = new Person("Mary");

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, childOne);
            relationships.AddParentAndChild(parent, childTwo);

            new Research(relationships);
        }
    }
}
