using CompanyStructureApi.Data;
using CompanyStructureApi.Dtos;
using CompanyStructureApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyStructureApi.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly AppDbContext _context;

		public EmployeeService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<Employee>> GetAllAsync()
		{
			return await _context.Employees
				.AsNoTracking()
				.OrderBy(e => e.LastName)
				.ThenBy(e => e.FirstName)
				.ToListAsync();
		}

		public async Task<Employee?> GetByIdAsync(int id)
		{
			return await _context.Employees
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<ServiceResult<Employee>> CreateAsync(EmployeeDto dto)
		{
			var email = dto.Email.Trim();

			var emailExists = await _context.Employees
				.AsNoTracking()
				.AnyAsync(e => e.Email == email);

			if (emailExists)
			{
				return new ServiceResult<Employee>(false, null, "Employee with this email already exists.");
			}

			var employee = new Employee
			{
				Title = dto.Title,
				FirstName = dto.FirstName.Trim(),
				LastName = dto.LastName.Trim(),
				Phone = dto.Phone,
				Email = email
			};

			_context.Employees.Add(employee);
			await _context.SaveChangesAsync();

			return new ServiceResult<Employee>(true, employee);
		}

		public async Task<ServiceResult> UpdateAsync(int id, EmployeeDto dto)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
			if (employee is null)
			{
				return new ServiceResult(false, "Employee not found.");
			}

			var email = dto.Email.Trim();

			var emailExists = await _context.Employees
				.AsNoTracking()
				.AnyAsync(e => e.Email == email && e.Id != id);

			if (emailExists)
			{
				return new ServiceResult(false, "Another employee with this email already exists.");
			}

			employee.Title = dto.Title;
			employee.FirstName = dto.FirstName.Trim();
			employee.LastName = dto.LastName.Trim();
			employee.Phone = dto.Phone;
			employee.Email = email;

			await _context.SaveChangesAsync();

			return new ServiceResult(true);
		}

		public async Task<ServiceResult> DeleteAsync(int id)
		{
			var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
			if (employee is null)
			{
				return new ServiceResult(false, "Employee not found.");
			}

			var isManager = await _context.OrgUnits
				.AsNoTracking()
				.AnyAsync(o => o.ManagerId == id);

			if (isManager)
			{
				return new ServiceResult(false, "Employee cannot be deleted because they are assigned as manager.");
			}

			_context.Employees.Remove(employee);
			await _context.SaveChangesAsync();

			return new ServiceResult(true);
		}
	}
}