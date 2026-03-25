using PersonalFinanceTracker.Domain.Dtos;

namespace PersonalFinanceTracker.Application.Interfaces
{
	public interface IAccountService
	{
		public Task<IReadOnlyList<AccountDto>> GetAsync(CancellationToken token = default);
		public Task<Guid> CreateAsync(AccountDto account, CancellationToken token = default);
		public Task<Guid> DeleteAsync(Guid accountId, CancellationToken token = default);
		public Task<AccountDto> GetByIdAsync(Guid accountId, CancellationToken token = default);
	}
}
