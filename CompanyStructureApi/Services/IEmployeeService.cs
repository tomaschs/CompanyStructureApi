using CompanyStructureApi.Dtos;
using CompanyStructureApi.Models;

namespace CompanyStructureApi.Services
{
	public interface IEmployeeService
	{
		Task<IReadOnlyList<Employee>> GetAllAsync();
		Task<Employee?> GetByIdAsync(int id);
		Task<ServiceResult<Employee>> CreateAsync(EmployeeDto dto);
		Task<ServiceResult> UpdateAsync(int id, EmployeeDto dto);
		Task<ServiceResult> DeleteAsync(int id);
	}
}