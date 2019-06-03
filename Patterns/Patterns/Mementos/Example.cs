using System;
using System.Collections.Generic;

namespace Patterns.Mementos
{
    public class Memento
    {
        public int Balance { get; }

        public Memento(int balance)
        {
            Balance = balance;
        }
    }

    public class BankAccount
    {
        private readonly List<Memento> changes = new List<Memento>();
        private int currentMemento;

        public int Balance { get; private set; }

        public BankAccount(int balance)
        {
            Balance = balance;
            changes.Add(new Memento(balance));
        }

        public Memento Deposit(int amount)
        {
            Balance += amount;
            var memento = new Memento(Balance);

            changes.Add(memento);
            ++currentMemento;

            return memento;
        }

        public Memento Withdraw(int amount)
        {
            Balance -= amount;
            var memento = new Memento(Balance);

            changes.Add(memento);
            ++currentMemento;

            return memento;
        }

        public Memento Restore(Memento memento)
        {
            if (memento != null)
            {
                Balance = memento.Balance;

                changes.Add(memento);
                ++currentMemento;

                return memento;
            }

            return null;
        }

        public Memento Undo()
        {
            if (currentMemento > 0)
            {
                var memento = changes[--currentMemento];
                Balance = memento.Balance;

                return memento;
            }

            return null;
        }

        public Memento Redo()
        {
            if (currentMemento + 1 < changes.Count)
            {
                var memento = changes[++currentMemento];
                Balance = memento.Balance;

                return memento;
            }

            return null;
        }

        public override string ToString() => $"{{ {nameof(Balance)} = {Balance} }}";
    }

    public static class Example
    {
        public static void Start()
        {
            var account = new BankAccount(1000);
            account.Deposit(500);
            account.Withdraw(250);

            Console.WriteLine($"Started with {account.Balance   }");

            account.Undo();
            Console.WriteLine($"Undo one: {account}");

            account.Undo();
            Console.WriteLine($"Undo two: {account}");

            account.Redo();
            Console.WriteLine($"Redo: {account}");
        }
    }
}
