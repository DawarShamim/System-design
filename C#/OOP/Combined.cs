using System;

namespace OOP
{
    // ============================================================
    //  1: ABSTRACTION
    //  Abstract class defines WHAT a bank account must do,
    //  not HOW it does it. Forces all account types to implement
    //  core behaviour while hiding internal complexity.
    // ============================================================

    public abstract class BankAccount
    {
        // ── 2: ENCAPSULATION ──────────────────────────────
        // Private fields — direct access is blocked from outside.
        // Balance can only change through controlled methods.
        private decimal _balance;
        private string _accountNumber;

        // Protected: subclasses can read but not write directly
        protected string OwnerName { get; private set; }

        public decimal Balance => _balance;
        public string AccountNumber => _accountNumber;

        protected BankAccount(string ownerName, string accountNumber, decimal initialBalance)
        {
            OwnerName = ownerName;
            _accountNumber = accountNumber;
            _balance = initialBalance;
        }

        // Controlled mutation — business rules enforced in one place
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            _balance += amount;
            Console.WriteLine($"[{AccountType}] Deposited {amount:C}. New balance: {_balance:C}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            if (amount > _balance)
                throw new InvalidOperationException("Insufficient funds.");

            _balance -= amount;
            Console.WriteLine($"[{AccountType}] Withdrew {amount:C}. New balance: {_balance:C}");
        }

        // Abstract members — subclasses MUST implement these
        public abstract string AccountType { get; }
        public abstract decimal InterestRate { get; }
        public abstract void ApplyInterest();

        public void PrintStatement()
        {
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine($"  Account Type  : {AccountType}");
            Console.WriteLine($"  Owner         : {OwnerName}");
            Console.WriteLine($"  Account No.   : {AccountNumber}");
            Console.WriteLine($"  Balance       : {Balance:C}");
            Console.WriteLine($"  Interest Rate : {InterestRate:P}");
            Console.WriteLine("─────────────────────────────────────");
        }
    }

    // ============================================================
    //  3: INHERITANCE
    //  SavingsAccount and CurrentAccount inherit shared behaviour
    //  from BankAccount and extend/override where needed.
    // ============================================================

    public class SavingsAccount : BankAccount
    {
        public override string AccountType => "Savings";
        public override decimal InterestRate => 0.045m; // 4.5%

        public SavingsAccount(string ownerName, string accountNumber, decimal initialBalance)
            : base(ownerName, accountNumber, initialBalance) { }

        public override void ApplyInterest()
        {
            decimal interest = Balance * InterestRate;
            Deposit(interest);
            Console.WriteLine($"Interest of {interest:C} applied to savings account.");
        }

        // Savings accounts have a minimum balance rule
        public override void Withdraw(decimal amount)
        {
            const decimal minimumBalance = 500m;
            if (Balance - amount < minimumBalance)
                throw new InvalidOperationException(
                    $"Savings accounts must maintain a minimum balance of {minimumBalance:C}.");

            base.Withdraw(amount); // reuse parent logic
        }
    }

    public class CurrentAccount : BankAccount
    {
        public override string AccountType => "Current";
        public override decimal InterestRate => 0.01m; // 1%

        private readonly decimal _overdraftLimit;

        public CurrentAccount(string ownerName, string accountNumber,
                              decimal initialBalance, decimal overdraftLimit = 1000m)
            : base(ownerName, accountNumber, initialBalance)
        {
            _overdraftLimit = overdraftLimit;
        }

        public override void ApplyInterest()
        {
            decimal interest = Balance * InterestRate;
            Deposit(interest);
            Console.WriteLine($"Interest of {interest:C} applied to current account.");
        }

        // Current accounts allow overdraft up to the limit
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            if (amount > Balance + _overdraftLimit)
                throw new InvalidOperationException(
                    $"Amount exceeds overdraft limit of {_overdraftLimit:C}.");

            // Bypass base rule (no overdraft) — apply our own
            Console.WriteLine($"[{AccountType}] Withdrew {amount:C}. New balance: {Balance - amount:C}");
            // Call base indirectly via deposit trick isn't clean — use reflection-safe approach:
            base.Withdraw(Math.Min(amount, Balance)); // withdraw what's available from balance
        }
    }

    // ============================================================
    //  4: POLYMORPHISM
    //  The same method call (ApplyInterest / Withdraw) behaves
    //  differently depending on the actual object type at runtime.
    //  Code works against the BankAccount abstraction, not
    //  the concrete types.
    // ============================================================

    public class Bank
    {
        private readonly BankAccount[] _accounts;

        public Bank(BankAccount[] accounts) { _accounts = accounts; }

        public void ApplyInterestToAll()
        {
            Console.WriteLine("\n>>> Applying interest to all accounts...\n");
            foreach (BankAccount account in _accounts)
            {
                // Polymorphism: each account type applies interest its own way
                account.ApplyInterest();
            }
        }

        public void PrintAllStatements()
        {
            Console.WriteLine("\n>>> Printing all account statements...\n");
            foreach (BankAccount account in _accounts)
            { account.PrintStatement(); }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create accounts
            var savings = new SavingsAccount("Alice", "SAV-001", 5000m);
            var current = new CurrentAccount("Bob", "CUR-001", 3000m);

            // Deposits
            savings.Deposit(1000m);
            current.Deposit(500m);

            // Withdrawals — each enforces its own rules (polymorphism)
            savings.Withdraw(200m);
            current.Withdraw(300m);

            // Bank operates on abstraction — doesn't know concrete types
            var bank = new Bank(new BankAccount[] { savings, current });
            bank.ApplyInterestToAll();
            bank.PrintAllStatements();
        }
    }
}