using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Entities;
using PersonalFinanceTracker.Domain.Helpers;
using PersonalFinanceTracker.Domain.Mapper;
using PersonalFinanceTracker.Infrastracture.Data;

namespace PersonalFinanceTracker.Application.Services
{
	public class AccountService(
		ApplicationDbContext dbContext, 
		ILogger<AccountService> logger) : IAccountService
	{
		public async Task<Guid> CreateAsync(AccountDto account, CancellationToken token = default)
		{
			try
			{
				Account entryEntity = AccountMapper.ToEntity(account);
				await dbContext.Accounts.AddAsync(entryEntity, token);
				await dbContext.SaveChangesAsync(token);
				return entryEntity.Id;
			}
			catch (Exception ex)
			{
				LoggerHelper.LogServiceException(logger, ex);
				throw;
			}
		}

		public async Task<Guid> DeleteAsync(Guid accountId, CancellationToken token = default)
		{
			try
			{
				Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId, token);
				if (account == null)
				{
					throw new KeyNotFoundException($"Account with id {accountId} not found.");
				}
				dbContext.Accounts.Remove(account);
				await dbContext.SaveChangesAsync(token);
				return accountId;
			}
			catch (Exception ex)
			{
				LoggerHelper.LogServiceException(logger, ex);
				throw;
			}
		}

		public async Task<IReadOnlyList<AccountDto>> GetAsync(CancellationToken token = default)
		{
			List<Account> accounts = await dbContext.Accounts.ToListAsync(token);
			return accounts.Select(AccountMapper.ToDto).ToList();
		}

		public async Task<AccountDto> GetByIdAsync(Guid accountId, CancellationToken token = default)
		{
			try
			{
				Account? account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == accountId, token);
				if (account == null)
				{
					throw new KeyNotFoundException($"Account with id {accountId} not found.");
				}
				return AccountMapper.ToDto(account);
			}
			catch (Exception ex)
			{
				LoggerHelper.LogServiceException(logger, ex);
				throw;
			}
		}
	}
}
