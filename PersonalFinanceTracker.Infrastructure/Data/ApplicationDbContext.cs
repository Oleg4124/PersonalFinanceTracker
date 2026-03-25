using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Domain.Entities;

namespace PersonalFinanceTracker.Infrastructure.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
		: DbContext(options)
	{
		public DbSet<Account> Accounts { get; set; }
	}
}
