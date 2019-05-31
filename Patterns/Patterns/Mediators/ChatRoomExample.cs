using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Mediators
{
    public class Person
    {
        private readonly List<string> chatLog = new List<string>();

        public string Name { get; set; }
        public ChatRoom Room { get; set; }

        public Person(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Say(string message) => Room.Broadcast(Name, message);

        public void PrivateMessage(string who, string message) => Room.Message(Name, who, message);

        public void Receive(string sender, string message)
        {
            var log = $"{sender}: {message}";
            chatLog.Add(log);

            Console.WriteLine($"[{Name}'s chat session] {log}");
        }
    }

    public class ChatRoom
    {
        private readonly List<Person> people = new List<Person>();

        public void Join(Person person)
        {
            var joinMsg = $"{person.Name} joins the chat";
            Broadcast("room", joinMsg);

            person.Room = this;
            people.Add(person);
        }

        public void Broadcast(string source, string message)
        {
            foreach (var p in people)
                if (p.Name != source)
                    p.Receive(source, message);
        }

        public void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)
                ?.Receive(source, message);
        }
    }

    public static class ChatRoomExample
    {
        public static void Start()
        {
            var room = new ChatRoom();

            var john = new Person("John");
            var jane = new Person("Jane");

            room.Join(john);
            room.Join(jane);

            john.Say("Hi");
            jane.Say("Hey John!");

            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("Hi everyone!");

            jane.PrivateMessage(simon.Name, "Glad you could join us.");
        }
    }
}
