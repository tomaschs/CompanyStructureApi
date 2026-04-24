using CompanyStructureApi.Dtos;
using CompanyStructureApi.Models;
using CompanyStructureApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStructureApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;

		public EmployeesController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
		{
			var employees = await _employeeService.GetAllAsync();
			return Ok(employees);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Employee>> GetById(int id)
		{
			var employee = await _employeeService.GetByIdAsync(id);
			if (employee is null)
			{
				return NotFound();
			}

			return Ok(employee);
		}

		[HttpPost]
		public async Task<ActionResult<Employee>> Create(EmployeeDto dto)
		{
			var result = await _employeeService.CreateAsync(dto);
			if (!result.Success)
			{
				return BadRequest(result.Error);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, EmployeeDto dto)
		{
			var result = await _employeeService.UpdateAsync(id, dto);
			if (!result.Success)
			{
				if (result.Error == "Employee not found.")
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
			var result = await _employeeService.DeleteAsync(id);
			if (!result.Success)
			{
				if (result.Error == "Employee not found.")
				{
					return NotFound();
				}

				return BadRequest(result.Error);
			}

			return NoContent();
		}
	}
}