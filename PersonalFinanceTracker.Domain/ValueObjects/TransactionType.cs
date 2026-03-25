using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PersonalFinanceTracker.Domain.ValueObjects
{
	public sealed record TransactionType
	{
		public string Value { get; }

		private TransactionType(string value) => Value = value;

		public static TransactionType Income => new("income");
		public static TransactionType Expense => new("expense");
		public static IReadOnlyCollection<TransactionType> All { get; } = new[]
		{
			Income, Expense
		};

		public static bool TryParse(string? value, [NotNullWhen(true)] out TransactionType? result)
		{
			result = null;

			if (string.IsNullOrWhiteSpace(value))
				return false;

			var normalized = value.Trim().ToLowerInvariant();

			result = normalized switch
			{
				"income" => Income,
				"expense" => Expense,
				_ => null
			};

			return result is not null;
		}

		public static TransactionType Parse(string value)
			=> TryParse(value, out var result)
				? result!
				: throw new ArgumentException(
					$"Invalid transaction type: '{value}'. Allowed values: income, expense.",
					nameof(value));

		public static TransactionType From(bool isIncome) => isIncome ? Income : Expense;

		public override string ToString() => Value;
	}
}
