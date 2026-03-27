using System;
using Xunit;

// ============================================================
//  Tests for OOP.cs — Bank Account (All 4 OOP Pillars)
//  Add to: C#/SOLID.Tests/OOPTest.cs
//  Run with: dotnet test (from C#/ folder)
// ============================================================

namespace OOP.Tests
{
    // ── 1. Encapsulation tests ────────────────────────────────────

    public class EncapsulationTests
    {
        [Fact]
        public void Balance_UpdatesCorrectly_AfterDeposit()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 1000m);
            account.Deposit(500m);
            Assert.Equal(1500m, account.Balance);
        }

        [Fact]
        public void Balance_UpdatesCorrectly_AfterWithdrawal()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 2000m);
            account.Withdraw(500m);
            Assert.Equal(1500m, account.Balance);
        }

        [Fact]
        public void Deposit_NegativeAmount_ThrowsArgumentException()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 1000m);
            Assert.Throws<ArgumentException>(() => account.Deposit(-100m));
        }

        [Fact]
        public void Deposit_ZeroAmount_ThrowsArgumentException()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 1000m);
            Assert.Throws<ArgumentException>(() => account.Deposit(0m));
        }

        [Fact]
        public void Balance_IsNotDirectlySettable()
        {
            // Encapsulation: Balance must be read-only from outside
            var balanceProperty = typeof(BankAccount).GetProperty("Balance");
            Assert.NotNull(balanceProperty);
            Assert.False(balanceProperty!.CanWrite);
        }

        [Fact]
        public void AccountNumber_IsNotDirectlySettable()
        {
            var prop = typeof(BankAccount).GetProperty("AccountNumber");
            Assert.NotNull(prop);
            Assert.False(prop!.CanWrite);
        }

        [Fact]
        public void Balance_field_IsPrivate()
        {
            // The backing _balance field must be private — not accessible outside
            var field = typeof(BankAccount).GetField("_balance",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);
            Assert.NotNull(field);
            Assert.True(field!.IsPrivate);
        }
    }

    // ── 2. Abstraction tests ──────────────────────────────────────

    public class AbstractionTests
    {
        [Fact]
        public void BankAccount_IsAbstract()
        {
            Assert.True(typeof(BankAccount).IsAbstract);
        }

        [Fact]
        public void BankAccount_CannotBeInstantiatedDirectly()
        {
            // Abstraction: you must use a concrete subclass
            var ctor = typeof(BankAccount).GetConstructors(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);
            Assert.Empty(ctor);
        }

        [Fact]
        public void AccountType_IsAbstract()
        {
            var prop = typeof(BankAccount).GetProperty("AccountType");
            Assert.NotNull(prop);
            Assert.True(prop!.GetMethod!.IsAbstract);
        }

        [Fact]
        public void InterestRate_IsAbstract()
        {
            var prop = typeof(BankAccount).GetProperty("InterestRate");
            Assert.NotNull(prop);
            Assert.True(prop!.GetMethod!.IsAbstract);
        }

        [Fact]
        public void ApplyInterest_IsAbstract()
        {
            var method = typeof(BankAccount).GetMethod("ApplyInterest");
            Assert.NotNull(method);
            Assert.True(method!.IsAbstract);
        }
    }

    // ── 3. Inheritance tests ──────────────────────────────────────

    public class InheritanceTests
    {
        [Fact]
        public void SavingsAccount_InheritsFromBankAccount()
        {
            Assert.True(typeof(SavingsAccount).IsSubclassOf(typeof(BankAccount)));
        }

        [Fact]
        public void CurrentAccount_InheritsFromBankAccount()
        {
            Assert.True(typeof(CurrentAccount).IsSubclassOf(typeof(BankAccount)));
        }

        [Fact]
        public void SavingsAccount_InheritsDeposit_FromBase()
        {
            // Deposit is defined in BankAccount — SavingsAccount reuses it
            var method = typeof(SavingsAccount).GetMethod("Deposit");
            Assert.NotNull(method);
            Assert.Equal(typeof(BankAccount), method!.DeclaringType);
        }

        [Fact]
        public void SavingsAccount_OverridesWithdraw()
        {
            // SavingsAccount has its own Withdraw with minimum balance rule
            var method = typeof(SavingsAccount).GetMethod("Withdraw");
            Assert.Equal(typeof(SavingsAccount), method!.DeclaringType);
        }

        [Fact]
        public void CurrentAccount_OverridesWithdraw()
        {
            var method = typeof(CurrentAccount).GetMethod("Withdraw");
            Assert.Equal(typeof(CurrentAccount), method!.DeclaringType);
        }

        [Fact]
        public void SavingsAccount_AccountType_ReturnsSavings()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 1000m);
            Assert.Equal("Savings", account.AccountType);
        }

        [Fact]
        public void CurrentAccount_AccountType_ReturnsCurrent()
        {
            var account = new CurrentAccount("Bob", "CUR-001", 1000m);
            Assert.Equal("Current", account.AccountType);
        }
    }

    // ── 4. Polymorphism tests ─────────────────────────────────────

    public class PolymorphismTests
    {
        [Fact]
        public void SavingsAccount_CanBeAssignedToBankAccountVariable()
        {
            // Polymorphism: subtype used wherever base type is expected
            BankAccount account = new SavingsAccount("Alice", "SAV-001", 1000m);
            Assert.NotNull(account);
        }

        [Fact]
        public void CurrentAccount_CanBeAssignedToBankAccountVariable()
        {
            BankAccount account = new CurrentAccount("Bob", "CUR-001", 1000m);
            Assert.NotNull(account);
        }

        [Fact]
        public void ApplyInterest_Savings_UsesSavingsRate()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 1000m);
            decimal expectedInterest = 1000m * account.InterestRate; // 45
            decimal balanceBefore = account.Balance;

            account.ApplyInterest();

            Assert.Equal(balanceBefore + expectedInterest, account.Balance);
        }

        [Fact]
        public void ApplyInterest_Current_UsesCurrentRate()
        {
            var account = new CurrentAccount("Bob", "CUR-001", 1000m);
            decimal expectedInterest = 1000m * account.InterestRate; // 10
            decimal balanceBefore = account.Balance;

            account.ApplyInterest();

            Assert.Equal(balanceBefore + expectedInterest, account.Balance);
        }

        [Fact]
        public void Bank_ApplyInterestToAll_CallsCorrectRatePerAccount()
        {
            var savings = new SavingsAccount("Alice", "SAV-001", 1000m);
            var current = new CurrentAccount("Bob", "CUR-001", 1000m);

            decimal savingsExpected = 1000m + (1000m * savings.InterestRate);
            decimal currentExpected = 1000m + (1000m * current.InterestRate);

            var bank = new Bank(new BankAccount[] { savings, current });
            bank.ApplyInterestToAll();

            Assert.Equal(savingsExpected, savings.Balance);
            Assert.Equal(currentExpected, current.Balance);
        }

        [Fact]
        public void Withdraw_Polymorphic_EachTypeEnforcesOwnRules()
        {
            // Savings enforces minimum balance — same method name, different behaviour
            var savings = new SavingsAccount("Alice", "SAV-001", 600m);
            Assert.Throws<InvalidOperationException>(() => savings.Withdraw(200m));

            // Current allows overdraft — same method name, different behaviour
            var current = new CurrentAccount("Bob", "CUR-001", 100m, overdraftLimit: 500m);
            var ex = Record.Exception(() => current.Withdraw(100m));
            Assert.Null(ex);
        }
    }

    // ── 5. SavingsAccount business rules ─────────────────────────

    public class SavingsAccountTests
    {
        [Fact]
        public void Withdraw_BelowMinimumBalance_ThrowsInvalidOperationException()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 600m);
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(200m));
        }

        [Fact]
        public void Withdraw_LeavesExactMinimumBalance_Succeeds()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 700m);
            var ex = Record.Exception(() => account.Withdraw(200m)); // leaves exactly 500
            Assert.Null(ex);
            Assert.Equal(500m, account.Balance);
        }

        [Fact]
        public void InterestRate_Is4Point5Percent()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 1000m);
            Assert.Equal(0.045m, account.InterestRate);
        }

        [Fact]
        public void ApplyInterest_IncreasesBalance_By4Point5Percent()
        {
            var account = new SavingsAccount("Alice", "SAV-001", 1000m);
            account.ApplyInterest();
            Assert.Equal(1045m, account.Balance);
        }
    }

    // ── 6. CurrentAccount business rules ─────────────────────────

    public class CurrentAccountTests
    {
        [Fact]
        public void Withdraw_WithinBalance_Succeeds()
        {
            var account = new CurrentAccount("Bob", "CUR-001", 1000m);
            var ex = Record.Exception(() => account.Withdraw(500m));
            Assert.Null(ex);
        }

        [Fact]
        public void Withdraw_ExceedsOverdraftLimit_ThrowsInvalidOperationException()
        {
            var account = new CurrentAccount("Bob", "CUR-001", 100m, overdraftLimit: 200m);
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(400m));
        }

        [Fact]
        public void InterestRate_Is1Percent()
        {
            var account = new CurrentAccount("Bob", "CUR-001", 1000m);
            Assert.Equal(0.01m, account.InterestRate);
        }

        [Fact]
        public void ApplyInterest_IncreasesBalance_By1Percent()
        {
            var account = new CurrentAccount("Bob", "CUR-001", 1000m);
            account.ApplyInterest();
            Assert.Equal(1010m, account.Balance);
        }

        [Fact]
        public void DefaultOverdraftLimit_Is1000()
        {
            // Start with 500 balance, withdraw 1000 — uses 500 balance + 500 overdraft
            // Total exposure is 1000 which is within the default 1000 overdraft limit
            var account = new CurrentAccount("Bob", "CUR-001", 500m);
            var ex = Record.Exception(() => account.Withdraw(1000m));
            Assert.Null(ex);
        }
    }
}