using System.ComponentModel.DataAnnotations;

namespace CompanyStructureApi.Dtos
{
	public class OrgUnitDto
	{
		[Required]
		[StringLength(200)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(50)]
		public string Code { get; set; } = null!;

		[Range(0, 3)]
		public int Type { get; set; }

		public int? ParentId { get; set; }
		public int? ManagerId { get; set; }
	}
}