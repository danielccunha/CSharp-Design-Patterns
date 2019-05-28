using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Commands
{
    public class BankAccount
    {
        private int balance;
        private readonly int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount:C}, balance is now {balance:C}");
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount:C}, balance is now {balance:C}");

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{{ {nameof(balance)}: {balance.ToString("C")} }}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
    }

    public class BankAccountCommand : ICommand
    {
        private readonly BankAccount account;
        private readonly Action action;
        private readonly int amount;
        private bool succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account ?? throw new ArgumentNullException(nameof(account));
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    succeeded = true;
                    break;
                case Action.Withdraw:
                    succeeded = account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!succeeded)
                return;

            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public enum Action
        {
            Deposit,
            Withdraw
        }
    }

    public static class Example
    {
        public static void Start()
        {
            var account = new BankAccount();
            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(account, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, 50),
            };

            foreach (var c in commands)
                c.Call();

            Console.WriteLine($"\n{account}\n");

            foreach (var c in Enumerable.Reverse(commands))
                c.Undo();

            Console.WriteLine($"\n{account}");
        }
    }
}
