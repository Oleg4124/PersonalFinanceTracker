namespace PersonalFinanceTracker.Domain.ValueObjects
{
	public sealed record AccountName
	{
		const int MaxLength = 25;
		const int MinLength = 1;
		public string Value { get; }
		private AccountName(string value) => Value = value;
		public static AccountName Create(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Account name cannot be empty.", nameof(name));
			string trimmed = name.Trim();
			if (MinLength >= trimmed.Length && trimmed.Length >= MaxLength)
				throw new ArgumentException($"Account name cannot has less {MinLength} and more {MaxLength} characters.", nameof(name));
			return new AccountName(trimmed);
		}
		public override string ToString() => Value;
	}
}