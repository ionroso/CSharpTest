using System;
using System.Collections.Generic;

namespace Design
{
    public enum TransactionResult
    {
        Success,
        InvalidAmount,
        InsufficientFunds,
        AccountNotFound,
        SameAccountTransferNotAllowed
    }

    public abstract class BankAccount
    {
        public string AccountNumber { get; }
        public string AccountHolderName { get; }
        public decimal Balance { get; private set; }

        protected BankAccount(string accountNumber, string accountHolderName, decimal initialBalance = 0)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number cannot be empty.", nameof(accountNumber));

            if (string.IsNullOrWhiteSpace(accountHolderName))
                throw new ArgumentException("Account holder name cannot be empty.", nameof(accountHolderName));

            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative.", nameof(initialBalance));

            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            Balance = initialBalance;
        }

        public TransactionResult Deposit(decimal amount)
        {
            if (amount <= 0)
                return TransactionResult.InvalidAmount;

            Balance += amount;
            return TransactionResult.Success;
        }

        public TransactionResult Withdraw(decimal amount)
        {
            if (amount <= 0)
                return TransactionResult.InvalidAmount;

            if (!CanWithdraw(amount))
                return TransactionResult.InsufficientFunds;

            Balance -= amount;
            return TransactionResult.Success;
        }

        protected abstract bool CanWithdraw(decimal amount);
    }

    public class SavingsAccount : BankAccount
    {
        public SavingsAccount(string accountNumber, string accountHolderName, decimal initialBalance = 0)
            : base(accountNumber, accountHolderName, initialBalance)
        {
        }

        protected override bool CanWithdraw(decimal amount)
        {
            return Balance >= amount;
        }
    }

    public class CheckingAccount : BankAccount
    {
        public decimal OverdraftLimit { get; }

        public CheckingAccount(string accountNumber, string accountHolderName, decimal initialBalance = 0, decimal overdraftLimit = 0)
            : base(accountNumber, accountHolderName, initialBalance)
        {
            if (overdraftLimit < 0)
                throw new ArgumentException("Overdraft limit cannot be negative.", nameof(overdraftLimit));

            OverdraftLimit = overdraftLimit;
        }

        protected override bool CanWithdraw(decimal amount)
        {
            return Balance + OverdraftLimit >= amount;
        }
    }

    public class Bank
    {
        private readonly Dictionary<string, BankAccount> _accounts = new();

        public void AddAccount(BankAccount account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (_accounts.ContainsKey(account.AccountNumber))
                throw new InvalidOperationException("Account with same number already exists.");

            _accounts[account.AccountNumber] = account;
        }

        public BankAccount? GetAccount(string accountNumber)
        {
            _accounts.TryGetValue(accountNumber, out var account);
            return account;
        }

        public TransactionResult Transfer(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            if (fromAccountNumber == toAccountNumber)
                return TransactionResult.SameAccountTransferNotAllowed;

            if (!_accounts.TryGetValue(fromAccountNumber, out var fromAccount))
                return TransactionResult.AccountNotFound;

            if (!_accounts.TryGetValue(toAccountNumber, out var toAccount))
                return TransactionResult.AccountNotFound;

            var withdrawResult = fromAccount.Withdraw(amount);
            if (withdrawResult != TransactionResult.Success)
                return withdrawResult;

            var depositResult = toAccount.Deposit(amount);

            if (depositResult != TransactionResult.Success)
            {
                // rollback for safety
                fromAccount.Deposit(amount);
                return depositResult;
            }

            return TransactionResult.Success;
        }
    }

    public class Program12
    {
        public static void Test()
        {
            var bank = new Bank();

            var savings = new SavingsAccount("S-100", "Alice", 1000m);
            var checking = new CheckingAccount("C-200", "Bob", 200m, overdraftLimit: 300m);

            bank.AddAccount(savings);
            bank.AddAccount(checking);

            Console.WriteLine($"Savings deposit: {savings.Deposit(200m)}");
            Console.WriteLine($"Savings withdraw: {savings.Withdraw(150m)}");
            Console.WriteLine($"Checking withdraw: {checking.Withdraw(400m)}");

            Console.WriteLine($"Transfer result: {bank.Transfer("S-100", "C-200", 300m)}");

            Console.WriteLine($"Savings balance: {savings.Balance}");
            Console.WriteLine($"Checking balance: {checking.Balance}");
        }
    }
}