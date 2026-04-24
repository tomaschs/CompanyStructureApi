using CompanyStructureApi.Data;
using CompanyStructureApi.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

try
{
	using SqlConnection conn = new SqlConnection(connectionString);
	conn.Open();
	Console.WriteLine("Database connection successful.");
}
catch (Exception ex)
{
	Console.WriteLine("Database connection failed: " + ex.Message);
}

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IOrgUnitService, OrgUnitService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	app.MapScalarApiReference(options =>
	{
		options.WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();