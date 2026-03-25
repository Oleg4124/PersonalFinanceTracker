using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Application.Interfaces;
using PersonalFinanceTracker.Domain.Dtos;

namespace PersonalFinanceTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController(IAccountService accountService) : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(typeof(List<AccountDto>), StatusCodes.Status200OK)]
		public async Task<IActionResult> Get(CancellationToken token)
		{
			IReadOnlyList<AccountDto> accounts = await accountService.GetAsync(token);
			return Ok(accounts);
		}

		[HttpPost]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		public async Task<IActionResult> Create([FromBody] AccountDto account, CancellationToken token)
		{
			Guid accountId = await accountService.CreateAsync(account, token);
			return CreatedAtAction(nameof(Create), new { accountId }, account);
		}

		[HttpDelete("{accountId:guid}")]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete(Guid accountId, CancellationToken token)
		{
			await accountService.DeleteAsync(accountId, token);
			return NoContent();
		}

		[HttpGet("{accountId:guid}")]
		[ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetById(Guid accountId, CancellationToken token)
		{
			AccountDto account = await accountService.GetByIdAsync(accountId, token);
			return Ok(account);
		}
	}
}