namespace CompanyStructureApi.Models
{
	public class OrgUnit
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Code { get; set; } = null!;
		public int Type { get; set; }
		public int? ParentId { get; set; }
		public int? ManagerId { get; set; }
	}
}