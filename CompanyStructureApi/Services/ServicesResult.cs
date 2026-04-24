namespace CompanyStructureApi.Services
{
	public record ServiceResult(bool Success, string? Error = null);

	public record ServiceResult<T>(bool Success, T? Data = default, string? Error = null);
}