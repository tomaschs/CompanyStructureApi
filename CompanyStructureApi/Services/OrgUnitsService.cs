using CompanyStructureApi.Data;
using CompanyStructureApi.Dtos;
using CompanyStructureApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyStructureApi.Services
{
	public class OrgUnitService : IOrgUnitService
	{
		private readonly AppDbContext _context;

		public OrgUnitService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IReadOnlyList<OrgUnit>> GetAllAsync()
		{
			return await _context.OrgUnits
				.AsNoTracking()
				.OrderBy(x => x.Type)
				.ThenBy(x => x.Name)
				.ToListAsync();
		}

		public async Task<OrgUnit?> GetByIdAsync(int id)
		{
			return await _context.OrgUnits
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<ServiceResult<OrgUnit>> CreateAsync(OrgUnitDto dto)
		{
			var validationError = await ValidateAsync(dto, null);
			if (validationError is not null)
			{
				return new ServiceResult<OrgUnit>(false, null, validationError);
			}

			var code = dto.Code.Trim();

			var codeExists = await _context.OrgUnits
				.AsNoTracking()
				.AnyAsync(x => x.Code == code);

			if (codeExists)
			{
				return new ServiceResult<OrgUnit>(false, null, "OrgUnit with this code already exists.");
			}

			var orgUnit = new OrgUnit
			{
				Name = dto.Name.Trim(),
				Code = code,
				Type = dto.Type,
				ParentId = dto.ParentId,
				ManagerId = dto.ManagerId
			};

			_context.OrgUnits.Add(orgUnit);
			await _context.SaveChangesAsync();

			return new ServiceResult<OrgUnit>(true, orgUnit);
		}

		public async Task<ServiceResult> UpdateAsync(int id, OrgUnitDto dto)
		{
			var orgUnit = await _context.OrgUnits.FirstOrDefaultAsync(x => x.Id == id);
			if (orgUnit is null)
			{
				return new ServiceResult(false, "OrgUnit not found.");
			}

			var validationError = await ValidateAsync(dto, id);
			if (validationError is not null)
			{
				return new ServiceResult(false, validationError);
			}

			var code = dto.Code.Trim();

			var codeExists = await _context.OrgUnits
				.AsNoTracking()
				.AnyAsync(x => x.Code == code && x.Id != id);

			if (codeExists)
			{
				return new ServiceResult(false, "Another OrgUnit with this code already exists.");
			}

			orgUnit.Name = dto.Name.Trim();
			orgUnit.Code = code;
			orgUnit.Type = dto.Type;
			orgUnit.ParentId = dto.ParentId;
			orgUnit.ManagerId = dto.ManagerId;

			await _context.SaveChangesAsync();

			return new ServiceResult(true);
		}

		public async Task<ServiceResult> DeleteAsync(int id)
		{
			var orgUnit = await _context.OrgUnits.FirstOrDefaultAsync(x => x.Id == id);
			if (orgUnit is null)
			{
				return new ServiceResult(false, "OrgUnit not found.");
			}

			var hasChildren = await _context.OrgUnits
				.AsNoTracking()
				.AnyAsync(x => x.ParentId == id);

			if (hasChildren)
			{
				return new ServiceResult(false, "OrgUnit cannot be deleted because it has child units.");
			}

			_context.OrgUnits.Remove(orgUnit);
			await _context.SaveChangesAsync();

			return new ServiceResult(true);
		}

		private async Task<string?> ValidateAsync(OrgUnitDto dto, int? currentId)
		{
			if (dto.Type < 0 || dto.Type > 3)
			{
				return "Invalid OrgUnit type. Allowed values: 0-3.";
			}

			if (!dto.ManagerId.HasValue)
			{
				return "ManagerId is required.";
			}

			var managerExists = await _context.Employees
				.AsNoTracking()
				.AnyAsync(x => x.Id == dto.ManagerId.Value);

			if (!managerExists)
			{
				return "Manager does not exist.";
			}

			if (!dto.ParentId.HasValue)
			{
				if (dto.Type != 0)
				{
					return "Root OrgUnit must be type 0 (Company).";
				}

				return null;
			}

			if (currentId.HasValue && dto.ParentId.Value == currentId.Value)
			{
				return "OrgUnit cannot be parent of itself.";
			}

			var parent = await _context.OrgUnits
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == dto.ParentId.Value);

			if (parent is null)
			{
				return "Parent OrgUnit does not exist.";
			}

			if (dto.Type != parent.Type + 1)
			{
				return "Invalid hierarchy. Allowed order: Company(0) -> Division(1) -> Project(2) -> Department(3).";
			}

			if (currentId.HasValue)
			{
				var parentId = dto.ParentId;

				while (parentId.HasValue)
				{
					if (parentId.Value == currentId.Value)
					{
						return "Hierarchy cycle detected.";
					}

					parentId = await _context.OrgUnits
						.AsNoTracking()
						.Where(x => x.Id == parentId.Value)
						.Select(x => x.ParentId)
						.FirstOrDefaultAsync();
				}
			}

			return null;
		}
	}
}