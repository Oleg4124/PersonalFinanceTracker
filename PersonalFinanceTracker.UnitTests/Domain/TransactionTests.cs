using FluentAssertions;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Domain.ValueObjects;

namespace PersonalFinanceTracker.UnitTests.Domain
{
	public class TransactionTests
	{
		[Fact]
		public void Ctor_should_throw_when_amount_is_zero_or_negative()
		{
			// Act
			Func<Transaction?> transaction = () => Transaction.Create(
				type: TransactionType.Expense,
				currency: Currency.USD,
				categoryId: Guid.NewGuid(),
				date: DateTimeOffset.UtcNow,
				note: "Test transaction",
				amount: 0m);

			// Assert
			transaction.Should().Throw<ArgumentOutOfRangeException>()
				.WithMessage("Amount must be greater than zero. (Parameter 'amount')");
		}

		[Fact]
		public void Ctor_should_create_transaction_with_expected_values()
		{
			// Arrange
			DateTimeOffset now = DateTimeOffset.Parse("2026-03-20T10:00:00+02:00");
			Guid categoryId = Guid.NewGuid();

			Transaction transaction = Transaction.Create(
				type: TransactionType.Expense,
				currency: Currency.USD,
				amount: 123.45m,
				categoryId: categoryId,
				date: now,
				note: "Food");

			// Assert
			transaction.Type.Should().Be(TransactionType.Expense);
			transaction.Currency.Should().Be(Currency.USD);
			transaction.Amount.Should().Be(123.45m);
			transaction.CategoryId.Should().Be(categoryId);
			transaction.Date.Should().Be(now);
			transaction.Note.Should().Be("Food");
		}
	}
}
