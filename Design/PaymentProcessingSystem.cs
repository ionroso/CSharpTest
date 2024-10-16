using System;
using System.Collections.Generic;
using System.Linq;

namespace Design
{
    // ============================================================================
    // RESULT MODELING
    // ============================================================================

    public enum PaymentStatus
    {
        Success,
        Failure
    }

    public sealed class PaymentResult
    {
        public PaymentStatus Status { get; }
        public string? TransactionId { get; }
        public string? FailureReason { get; }

        public PaymentResult(
            PaymentStatus status,
            string? transactionId = null,
            string? failureReason = null)
        {
            Status = status;
            TransactionId = transactionId;
            FailureReason = failureReason;
        }

        public bool IsSuccessful() => Status == PaymentStatus.Success;

        public override string ToString()
        {
            return IsSuccessful()
                ? $"PaymentResult(SUCCESS, transaction_id={TransactionId})"
                : $"PaymentResult(FAILURE, reason={FailureReason})";
        }
    }

    // ============================================================================
    // PAYMENT DATA MODELS
    // ============================================================================

    public abstract class Payment
    {
        public decimal Amount { get; }
        public string Currency { get; }
        public string Description { get; }

        protected Payment(decimal amount, string currency, string description = "")
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));

            Amount = amount;
            Currency = currency;
            Description = description ?? string.Empty;
        }
    }

    public sealed class CreditCardPayment : Payment
    {
        public string CardNumber { get; }
        public string CardholderName { get; }
        public string CVV { get; }

        public CreditCardPayment(
            decimal amount,
            string currency,
            string cardNumber,
            string cardholderName,
            string cvv,
            string description = "")
            : base(amount, currency, description)
        {
            CardNumber = cardNumber ?? string.Empty;
            CardholderName = cardholderName ?? string.Empty;
            CVV = cvv ?? string.Empty;
        }
    }

    public sealed class PayPalPayment : Payment
    {
        public string PayPalEmail { get; }

        public PayPalPayment(
            decimal amount,
            string currency,
            string payPalEmail,
            string description = "")
            : base(amount, currency, description)
        {
            PayPalEmail = payPalEmail ?? string.Empty;
        }
    }

    public sealed class BankTransferPayment : Payment
    {
        public string AccountNumber { get; }
        public string RoutingNumber { get; }

        public BankTransferPayment(
            decimal amount,
            string currency,
            string accountNumber,
            string routingNumber,
            string description = "")
            : base(amount, currency, description)
        {
            AccountNumber = accountNumber ?? string.Empty;
            RoutingNumber = routingNumber ?? string.Empty;
        }
    }

    // ============================================================================
    // PROCESSING LAYER
    // ============================================================================

    public interface IPaymentProcessor
    {
        bool CanHandle(Payment payment);
        PaymentResult Process(Payment payment);
    }

    public abstract class PaymentProcessorBase<TPayment> : IPaymentProcessor
        where TPayment : Payment
    {
        public bool CanHandle(Payment payment) => payment is TPayment;

        PaymentResult IPaymentProcessor.Process(Payment payment)
        {
            if (payment is not TPayment typedPayment)
            {
                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: $"Invalid payment type for {GetType().Name}");
            }

            if (!Validate(typedPayment))
            {
                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: "Validation failed");
            }

            return Process(typedPayment);
        }

        public abstract PaymentResult Process(TPayment payment);

        protected abstract bool Validate(TPayment payment);

        protected string GenerateTransactionId()
        {
            return $"TXN-{Guid.NewGuid():N}"[..16];
        }
    }

    public sealed class CreditCardProcessor : PaymentProcessorBase<CreditCardPayment>
    {
        protected override bool Validate(CreditCardPayment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.CardNumber)) return false;
            if (string.IsNullOrWhiteSpace(payment.CardholderName)) return false;
            if (string.IsNullOrWhiteSpace(payment.CVV)) return false;

            return true;
        }

        public override PaymentResult Process(CreditCardPayment payment)
        {
            try
            {
                string transactionId = GenerateTransactionId();
                return new PaymentResult(PaymentStatus.Success, transactionId);
            }
            catch (Exception ex)
            {
                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: $"Processing error: {ex.Message}");
            }
        }
    }

    public sealed class PayPalProcessor : PaymentProcessorBase<PayPalPayment>
    {
        protected override bool Validate(PayPalPayment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.PayPalEmail)) return false;
            return true;
        }

        public override PaymentResult Process(PayPalPayment payment)
        {
            try
            {
                string transactionId = GenerateTransactionId();

                if (AuthorizePayPalPayment(payment))
                {
                    return new PaymentResult(PaymentStatus.Success, transactionId);
                }

                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: "PayPal authorization failed");
            }
            catch (Exception ex)
            {
                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: $"Processing error: {ex.Message}");
            }
        }

        private static bool AuthorizePayPalPayment(PayPalPayment payment)
        {
            return true;
        }
    }

    public sealed class BankTransferProcessor : PaymentProcessorBase<BankTransferPayment>
    {
        protected override bool Validate(BankTransferPayment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.AccountNumber)) return false;
            if (string.IsNullOrWhiteSpace(payment.RoutingNumber)) return false;

            return true;
        }

        public override PaymentResult Process(BankTransferPayment payment)
        {
            try
            {
                string transactionId = GenerateTransactionId();

                if (InitiateBankTransfer(payment))
                {
                    return new PaymentResult(PaymentStatus.Success, transactionId);
                }

                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: "Bank transfer initiation failed");
            }
            catch (Exception ex)
            {
                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: $"Processing error: {ex.Message}");
            }
        }

        private static bool InitiateBankTransfer(BankTransferPayment payment)
        {
            return true;
        }
    }

    // ============================================================================
    // MAIN PAYMENT SERVICE
    // ============================================================================

    public sealed class PaymentService
    {
        private readonly IReadOnlyList<IPaymentProcessor> _processors;

        public PaymentService(IEnumerable<IPaymentProcessor> processors)
        {
            if (processors == null)
                throw new ArgumentNullException(nameof(processors));

            _processors = processors.ToList().AsReadOnly();
        }

        public PaymentResult ProcessPayment(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            var processor = _processors.FirstOrDefault(p => p.CanHandle(payment));

            if (processor == null)
            {
                return new PaymentResult(
                    PaymentStatus.Failure,
                    failureReason: $"No processor found for payment type {payment.GetType().Name}");
            }

            return processor.Process(payment);
        }
        public static void Test()
        {
            var service = new PaymentService(new IPaymentProcessor[]
            {
                new CreditCardProcessor(),
                new PayPalProcessor(),
                new BankTransferProcessor()
            });

            Console.WriteLine("=".PadRight(70, '='));
            Console.WriteLine("PAYMENT PROCESSING SYSTEM - FINAL VERSION");
            Console.WriteLine("=".PadRight(70, '='));
            Console.WriteLine();

            Console.WriteLine("[Example 1] Valid Bank Transfer");
            Console.WriteLine("-".PadRight(70, '-'));
            var bankPayment = new BankTransferPayment(
                amount: 500.00m,
                currency: "USD",
                accountNumber: "12345678",
                routingNumber: "987654321"
            );
            var result = service.ProcessPayment(bankPayment);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();

            Console.WriteLine("[Example 2] Valid Credit Card");
            Console.WriteLine("-".PadRight(70, '-'));
            var ccPayment = new CreditCardPayment(
                amount: 150.00m,
                currency: "USD",
                cardNumber: "4532015112830366",
                cardholderName: "John Doe",
                cvv: "123"
            );
            result = service.ProcessPayment(ccPayment);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine();

            Console.WriteLine("[Example 3] Invalid Bank Transfer (missing routing number)");
            Console.WriteLine("-".PadRight(70, '-'));
            var invalidPayment = new BankTransferPayment(
                amount: 100.00m,
                currency: "USD",
                accountNumber: "12345678",
                routingNumber: ""
            );
            result = service.ProcessPayment(invalidPayment);
            Console.WriteLine($"Result: {result}");
        }
    }
}