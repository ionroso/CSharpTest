using System;
using System.Collections.Generic;

namespace Design1
{
    public enum AtmOperationResult
    {
        Success,
        NoCardInserted,
        AuthenticationRequired,
        InvalidPin,
        AccountNotFound,
        InvalidAmount,
        InsufficientFunds,
        AtmOutOfCash
    }

    public class BankAccount
    {
        public string AccountNumber { get; }
        public string AccountHolderName { get; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountNumber, string accountHolderName, decimal initialBalance = 0)
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

        public AtmOperationResult Deposit(decimal amount)
        {
            if (amount <= 0)
                return AtmOperationResult.InvalidAmount;

            Balance += amount;
            return AtmOperationResult.Success;
        }

        public AtmOperationResult Withdraw(decimal amount)
        {
            if (amount <= 0)
                return AtmOperationResult.InvalidAmount;

            if (Balance < amount)
                return AtmOperationResult.InsufficientFunds;

            Balance -= amount;
            return AtmOperationResult.Success;
        }
    }

    public class Card
    {
        public string CardNumber { get; }
        public string AccountNumber { get; }
        public string Pin { get; }

        public Card(string cardNumber, string accountNumber, string pin)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number cannot be empty.", nameof(cardNumber));

            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number cannot be empty.", nameof(accountNumber));

            if (string.IsNullOrWhiteSpace(pin))
                throw new ArgumentException("PIN cannot be empty.", nameof(pin));

            CardNumber = cardNumber;
            AccountNumber = accountNumber;
            Pin = pin;
        }
    }

    public class Bank
    {
        private readonly Dictionary<string, BankAccount> _accounts = new();
        private readonly Dictionary<string, Card> _cards = new();

        public void AddAccount(BankAccount account)
        {
            _accounts[account.AccountNumber] = account;
        }

        public void AddCard(Card card)
        {
            _cards[card.CardNumber] = card;
        }

        public bool ValidatePin(string cardNumber, string pin)
        {
            return _cards.TryGetValue(cardNumber, out var card) && card.Pin == pin;
        }

        public BankAccount? GetAccountByCard(string cardNumber)
        {
            if (!_cards.TryGetValue(cardNumber, out var card))
                return null;

            _accounts.TryGetValue(card.AccountNumber, out var account);
            return account;
        }

        /* inline
        public BankAccount? GetAccountByCard(string cardNumber)
{
    return _cards.TryGetValue(cardNumber, out var card) &&
           _accounts.TryGetValue(card.AccountNumber, out var account)
        ? account
        : null;
}

         */
    }

    public class ATM
    {
        private readonly Bank _bank;

        public decimal CashAvailable { get; private set; }
        public Card? InsertedCard { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public ATM(Bank bank, decimal initialCash)
        {
            _bank = bank ?? throw new ArgumentNullException(nameof(bank));

            if (initialCash < 0)
                throw new ArgumentException("Initial cash cannot be negative.", nameof(initialCash));

            CashAvailable = initialCash;
        }

        public void InsertCard(Card card)
        {
            InsertedCard = card ?? throw new ArgumentNullException(nameof(card));
            IsAuthenticated = false;
        }

        public void EjectCard()
        {
            InsertedCard = null;
            IsAuthenticated = false;
        }

        public AtmOperationResult EnterPin(string pin)
        {
            if (InsertedCard == null)
                return AtmOperationResult.NoCardInserted;

            if (!_bank.ValidatePin(InsertedCard.CardNumber, pin))
                return AtmOperationResult.InvalidPin;

            IsAuthenticated = true;
            return AtmOperationResult.Success;
        }

        public (AtmOperationResult Result, decimal? Balance) CheckBalance()
        {
            if (InsertedCard == null)
                return (AtmOperationResult.NoCardInserted, null);

            if (!IsAuthenticated)
                return (AtmOperationResult.AuthenticationRequired, null);

            var account = _bank.GetAccountByCard(InsertedCard.CardNumber);
            if (account == null)
                return (AtmOperationResult.AccountNotFound, null);

            return (AtmOperationResult.Success, account.Balance);
        }

        public AtmOperationResult Deposit(decimal amount)
        {
            if (InsertedCard == null)
                return AtmOperationResult.NoCardInserted;

            if (!IsAuthenticated)
                return AtmOperationResult.AuthenticationRequired;

            var account = _bank.GetAccountByCard(InsertedCard.CardNumber);
            if (account == null)
                return AtmOperationResult.AccountNotFound;

            var result = account.Deposit(amount);
            if (result == AtmOperationResult.Success)
            {
                CashAvailable += amount;
            }

            return result;
        }

        public AtmOperationResult Withdraw(decimal amount)
        {
            if (InsertedCard == null)
                return AtmOperationResult.NoCardInserted;

            if (!IsAuthenticated)
                return AtmOperationResult.AuthenticationRequired;

            if (amount <= 0)
                return AtmOperationResult.InvalidAmount;

            if (CashAvailable < amount)
                return AtmOperationResult.AtmOutOfCash;

            var account = _bank.GetAccountByCard(InsertedCard.CardNumber);
            if (account == null)
                return AtmOperationResult.AccountNotFound;

            var result = account.Withdraw(amount);
            if (result != AtmOperationResult.Success)
                return result;

            CashAvailable -= amount;
            return AtmOperationResult.Success;
        }
    }

    public class Program56
    {
        public static void Test()
        {
            var bank = new Bank();

            var account = new BankAccount("A-100", "Alice", 1000m);
            var card = new Card("CARD-1", "A-100", "1234");

            bank.AddAccount(account);
            bank.AddCard(card);

            var atm = new ATM(bank, 5000m);

            atm.InsertCard(card);

            Console.WriteLine($"PIN result: {atm.EnterPin("1234")}");
            Console.WriteLine($"Withdraw result: {atm.Withdraw(200m)}");

            var balanceResult = atm.CheckBalance();
            Console.WriteLine($"Balance result: {balanceResult.Result}, Balance: {balanceResult.Balance}");

            atm.EjectCard();
        }
    }
}