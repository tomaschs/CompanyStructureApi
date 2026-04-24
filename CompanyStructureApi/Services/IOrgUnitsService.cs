using CompanyStructureApi.Dtos;
using CompanyStructureApi.Models;

namespace CompanyStructureApi.Services
{
	public interface IOrgUnitService
	{
		Task<IReadOnlyList<OrgUnit>> GetAllAsync();
		Task<OrgUnit?> GetByIdAsync(int id);
		Task<ServiceResult<OrgUnit>> CreateAsync(OrgUnitDto dto);
		Task<ServiceResult> UpdateAsync(int id, OrgUnitDto dto);
		Task<ServiceResult> DeleteAsync(int id);
	}
}