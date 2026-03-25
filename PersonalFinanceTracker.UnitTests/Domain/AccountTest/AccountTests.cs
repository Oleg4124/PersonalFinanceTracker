using FluentAssertions;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.UnitTests.Domain.AccountTest
{
	public class AccountTests
	{
		[Fact]
		public void Create_ShouldInitializeBalanceToZero()
		{
			// Arrange
			string accountName = "Account Test";
			// Act
			Account account = Account.Create(accountName);
			// Assert
			account.Balance.Should().Be(0m);
		}

		[Fact]
		public void Create_ShouldInitializeTransactionsCollectionEmpty()
		{
			// Arrange
			string accountName = "Account Test";
			// Act
			Account account = Account.Create(accountName);
			// Assert
			account.Transactions.Should().BeEmpty();
		}
	}
}
