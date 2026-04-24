using System.ComponentModel.DataAnnotations;

namespace CompanyStructureApi.Dtos
{
	public class EmployeeDto
	{
		[StringLength(50)]
		public string? Title { get; set; }

		[Required]
		[StringLength(100)]
		public string FirstName { get; set; } = null!;

		[Required]
		[StringLength(100)]
		public string LastName { get; set; } = null!;

		[Phone]
		[StringLength(30)]
		public string? Phone { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(200)]
		public string Email { get; set; } = null!;
	}
}