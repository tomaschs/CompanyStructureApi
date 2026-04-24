using Microsoft.EntityFrameworkCore;
using CompanyStructureApi.Models;

namespace CompanyStructureApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<OrgUnit> OrgUnits { get; set; }
	}
}