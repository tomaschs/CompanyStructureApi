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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Employee>().HasData(
				new Employee { Id = 1, Title = "CEO", FirstName = "Peter", LastName = "Novak", Phone = "0900123456", Email = "ceo@company.com" },
				new Employee { Id = 2, Title = "Lead", FirstName = "Jana", LastName = "Hruba", Phone = "0900765432", Email = "jana.hruba@company.com" },
				new Employee { Id = 3, Title = "Lead", FirstName = "Martin", LastName = "Kovac", Phone = "0911122334", Email = "martin.kovac@company.com" },
				new Employee { Id = 4, Title = "Mgr", FirstName = "Eva", LastName = "Bielikova", Phone = "0944556677", Email = "eva.bielikova@company.com" },
				new Employee { Id = 5, Title = "Ing", FirstName = "Adam", LastName = "Sokol", Phone = "0911223344", Email = "adam.sokol@company.com" },
				new Employee { Id = 6, Title = "Bc", FirstName = "Lucia", LastName = "Kralova", Phone = "0909988776", Email = "lucia.kralova@company.com" }
			);

			modelBuilder.Entity<OrgUnit>().HasData(
				// Company
				new OrgUnit { Id = 1, Name = "Tech Solutions s.r.o.", Code = "TS", Type = 0, ParentId = null, ManagerId = 1 },

				// Divisions
				new OrgUnit { Id = 2, Name = "Software Development", Code = "DEV", Type = 1, ParentId = 1, ManagerId = 2 },
				new OrgUnit { Id = 3, Name = "IT Operations", Code = "OPS", Type = 1, ParentId = 1, ManagerId = 3 },

				// Projects
				new OrgUnit { Id = 4, Name = "ERP System", Code = "ERP", Type = 2, ParentId = 2, ManagerId = 4 },
				new OrgUnit { Id = 5, Name = "Cloud Migration", Code = "CLOUD", Type = 2, ParentId = 3, ManagerId = 5 },

				// Departments
				new OrgUnit { Id = 6, Name = "Backend Team", Code = "BE", Type = 3, ParentId = 4, ManagerId = 6 },
				new OrgUnit { Id = 7, Name = "DevOps Team", Code = "DO", Type = 3, ParentId = 5, ManagerId = 3 }
			);
		}
	}
}