using CompanyStructureApi.Dtos;
using CompanyStructureApi.Models;
using CompanyStructureApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStructureApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrgUnitsController : ControllerBase
	{
		private readonly IOrgUnitService _orgUnitService;

		public OrgUnitsController(IOrgUnitService orgUnitService)
		{
			_orgUnitService = orgUnitService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrgUnit>>> GetAll()
		{
			var items = await _orgUnitService.GetAllAsync();
			return Ok(items);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<OrgUnit>> GetById(int id)
		{
			var item = await _orgUnitService.GetByIdAsync(id);
			if (item is null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		[HttpPost]
		public async Task<ActionResult<OrgUnit>> Create(OrgUnitDto dto)
		{
			var result = await _orgUnitService.CreateAsync(dto);
			if (!result.Success)
			{
				return BadRequest(result.Error);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, OrgUnitDto dto)
		{
			var result = await _orgUnitService.UpdateAsync(id, dto);
			if (!result.Success)
			{
				if (result.Error == "OrgUnit not found.")
				{
					return NotFound();
				}

				return BadRequest(result.Error);
			}

			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _orgUnitService.DeleteAsync(id);
			if (!result.Success)
			{
				if (result.Error == "OrgUnit not found.")
				{
					return NotFound();
				}

				return BadRequest(result.Error);
			}

			return NoContent();
		}
	}
}