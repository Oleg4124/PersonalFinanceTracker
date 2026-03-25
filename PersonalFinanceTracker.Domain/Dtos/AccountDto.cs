using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Domain.Dtos
{
	public class AccountDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Balance { get; set; }
	}
}
