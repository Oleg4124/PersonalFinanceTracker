namespace PersonalFinanceTracker.Domain.Entities
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool IsIncome { get; set; }

		public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

		private Category(string name, bool isIncome)
		{
			Id = Guid.NewGuid();
			Name = name;
			IsIncome = isIncome;
		}
		public static Category Create(string name, bool isIncome)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Category name cannot be empty.", nameof(name));
			return new Category(name.Trim(), isIncome);
		}
	}
}
