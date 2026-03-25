using PersonalFinanceTracker.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace PersonalFinanceTracker.Domain.Entities
{
	public class Account
	{
		public Guid Id { get; set; }
		public AccountName Name { get; set; }
		public decimal Balance { get; private set; }
		private readonly List<Transaction> _transactions = new();
		public IReadOnlyCollection<Transaction> Transactions => new ReadOnlyCollection<Transaction>(_transactions);
		private Account(string name)
		{
			Id = Guid.NewGuid();
			Name = AccountName.Create(name);
		}
		public static Account Create(string name) => new Account(name);

		public Transaction AddTransaction(
			TransactionType type,
			Currency currency,
			Guid categoryId,
			DateTimeOffset date,
			string? note,
			decimal amount)
		{
			Transaction transaction = Transaction.Create(type, currency, categoryId, date, note, amount);

			_transactions.Add(transaction);

			ApplyToBalance(transaction);

			return transaction;
		}

		public void RemoveTransaction(Guid transactionId)
		{
			Transaction? transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
			if (transaction == null) return;

			_transactions.Remove(transaction);

			RevertFromBalance(transaction);
		}

		private void ApplyToBalance(Transaction transaction)
		{
			Balance += transaction.Type == TransactionType.Income ? 
					   transaction.Amount : 
					   -transaction.Amount;
		}

		private void RevertFromBalance(Transaction transaction)
		{
			Balance -= transaction.Type == TransactionType.Income ? 
					   transaction.Amount : 
					   -transaction.Amount;
		}
	}
}
