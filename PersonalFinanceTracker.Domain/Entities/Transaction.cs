using PersonalFinanceTracker.Domain.ValueObjects;

namespace PersonalFinanceTracker.Domain.Entities
{
	public class Transaction
	{
		public Guid Id { get; private set; }
		public TransactionType Type { get; private set; } = TransactionType.Expense;
		public Currency Currency { get; private set;  } = Currency.UAH;
		public decimal Amount { get; private set; }
		public Guid CategoryId { get; private set; }
		public DateTimeOffset Date { get; private set; } = DateTimeOffset.UtcNow;
		public string? Note { get; private set; }

		public virtual Category Category { get; set; } = null!;

		private Transaction() { }
		private Transaction(
			TransactionType type, 
			Currency currency, 
			Guid categoryId, 
			DateTimeOffset date, 
			string? note, 
			decimal amount)
		{
			if (amount <= 0)
			{
				throw new ArgumentOutOfRangeException(
					paramName: nameof(amount), 
					message: "Amount must be greater than zero.");
			}

			Id = Guid.NewGuid();
			Type = type;
			Currency = currency;
			CategoryId = categoryId;
			Date = date;
			Note = note;
			Amount = amount;
		}

		public static Transaction Create(
			TransactionType type, 
			Currency currency, 
			Guid categoryId, 
			DateTimeOffset date, 
			string? note, 
			decimal amount)
			=> new(type, currency, categoryId, date, note, amount);
	}
}
