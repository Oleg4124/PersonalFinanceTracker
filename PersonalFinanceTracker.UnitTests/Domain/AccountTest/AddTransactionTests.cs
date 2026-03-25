using FluentAssertions;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Domain.ValueObjects;

namespace PersonalFinanceTracker.UnitTests.Domain.AccountTest
{
	public class AddTransactionTests
	{
		[Fact]
		public void AddTransaction_WhenIncome_ShouldIncreaseBalanceByAmount()
		{
			// Arrange
			Account account = Account.Create("Test Account");
			decimal amount = 100m;
			// Act
			account.AddTransaction(
				type: TransactionType.Income,
				currency: Currency.USD,
				categoryId: Guid.NewGuid(),
				date: DateTimeOffset.UtcNow,
				note: "Test income",
				amount: amount);
			// Assert
			account.Balance.Should().Be(amount);
		}

		[Fact]
		public void AddTransaction_WhenExpense_ShouldDecreaseBalanceByAmount()
		{
			// Arrange
			Account account = Account.Create("Test Account");
			decimal amount = 50m;
			// Act
			account.AddTransaction(
				type: TransactionType.Expense,
				currency: Currency.USD,
				categoryId: Guid.NewGuid(),
				date: DateTimeOffset.UtcNow,
				note: "Test expense",
				amount: amount);
			// Assert
			account.Balance.Should().Be(-amount);
		}

		[Fact]
		public void AddTransaction_ShouldAddTransactionToCollection()
		{
			// Arrange
			Account account = Account.Create("Test Account");
			decimal amount = 50m;
			// Act
			account.AddTransaction(
				type: TransactionType.Expense,
				currency: Currency.USD,
				categoryId: Guid.NewGuid(),
				date: DateTimeOffset.UtcNow,
				note: "Test expense",
				amount: amount);
			// Assert
			account.Transactions.Count.Should().Be(1);
		}

		[Fact]
		public void AddTransaction_ShouldReturnCreatedTransaction()
		{
			// Arrange
			Account account = Account.Create("Test Account");
			decimal amount = 50m;
			// Act
			Transaction transaction = account.AddTransaction(
				type: TransactionType.Expense,
				currency: Currency.USD,
				categoryId: Guid.NewGuid(),
				date: DateTimeOffset.UtcNow,
				note: "Test expense",
				amount: amount);
			// Assert
			transaction.Should().NotBeNull();
			transaction.Type.Should().Be(TransactionType.Expense);
			transaction.Currency.Should().Be(Currency.USD);
			transaction.Amount.Should().Be(amount);
			transaction.Note.Should().Be("Test expense");
			transaction.Date.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
			transaction.CategoryId.Should().NotBeEmpty();
			transaction.Id.Should().NotBeEmpty();
		}

		[Fact]
		public void AddTransaction_WhenAmountIsZero_ShouldThrowArgumentOutOfRangeException()
		{
			// Arrange
			Account account = Account.Create("Test Account");
			// Act
			Action act = () => account.AddTransaction(
				type: TransactionType.Expense,
				currency: Currency.USD,
				categoryId: Guid.NewGuid(),
				date: DateTimeOffset.UtcNow,
				note: "Test expense",
				amount: 0m);
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>()
				.WithMessage("Amount must be greater than zero. (Parameter 'amount')");
		}

		[Fact]
		public void AddTransaction_WhenAmountIsNegative_ShouldThrowArgumentOutOfRangeException()
		{ 			
			// Arrange
			Account account = Account.Create("Test Account");
			// Act
			Action act = () => account.AddTransaction(
				type: TransactionType.Expense,
				currency: Currency.USD,
				categoryId: Guid.NewGuid(),
				date: DateTimeOffset.UtcNow,
				note: "Test expense",
				amount: -10m);
			// Assert
			act.Should().Throw<ArgumentOutOfRangeException>()
				.WithMessage("Amount must be greater than zero. (Parameter 'amount')");
		}

		[Fact]
		public void AddTransaction_WhenCategoryIdIsEmpty_ShouldThrowArgumentException()
		{
			// Arrange
			Account account = Account.Create("Test Account");
			// Act
			Action act = () => account.AddTransaction(
				type: TransactionType.Expense,
				currency: Currency.USD,
				categoryId: Guid.Empty,
				date: DateTimeOffset.UtcNow,
				note: "Test expense",
				amount: 10m);
			// Assert
			act.Should().Throw<ArgumentException>()
				.WithMessage("Category ID cannot be empty. (Parameter 'categoryId')");
		}


	}
}
