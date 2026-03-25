using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Domain.Mapper
{
	public static class AccountMapper
	{
		public static AccountDto ToDto(Account entity)
			=> new AccountDto
			{
				Id = entity.Id,
				Name = entity.Name.Value,
				Balance = entity.Balance
			};

		public static Account ToEntity(AccountDto dto) => Account.Create(dto.Name);
	}
}
