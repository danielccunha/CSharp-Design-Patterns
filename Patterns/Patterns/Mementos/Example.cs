using System;
using System.Collections.Generic;
using System.Text;

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
        public int Balance { get; private set; }

        public BankAccount(int balance)
        {
            Balance = balance;
        }

        public Memento Deposit(int amount)
        {
            Balance += amount;
            return new Memento(Balance);
        }

        public Memento Withdraw(int amount)
        {
            Balance -= amount;
            return new Memento(Balance);
        }

        public void Restore(Memento memento)
        {
            Balance = memento.Balance;
        }
    }

    public static class Example
    {
        public static void Start()
        {
            var account = new BankAccount(1000);
            var m1 = account.Deposit(500);
            var m2 = account.Withdraw(250);

            Console.WriteLine(account.Balance);

            account.Restore(m1);
            Console.WriteLine(account.Balance);

            account.Restore(m2);
            Console.WriteLine(account.Balance);
        }
    }
}
