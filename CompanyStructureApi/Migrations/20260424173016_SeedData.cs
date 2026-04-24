using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyStructureApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Phone", "Title" },
                values: new object[,]
                {
                    { 1, "ceo@company.com", "Peter", "Novak", "0900123456", "CEO" },
                    { 2, "jana.hruba@company.com", "Jana", "Hruba", "0900765432", "Lead" },
                    { 3, "martin.kovac@company.com", "Martin", "Kovac", "0911122334", "Lead" },
                    { 4, "eva.bielikova@company.com", "Eva", "Bielikova", "0944556677", "Mgr" },
                    { 5, "adam.sokol@company.com", "Adam", "Sokol", "0911223344", "Ing" },
                    { 6, "lucia.kralova@company.com", "Lucia", "Kralova", "0909988776", "Bc" }
                });

            migrationBuilder.InsertData(
                table: "OrgUnits",
                columns: new[] { "Id", "Code", "ManagerId", "Name", "ParentId", "Type" },
                values: new object[,]
                {
                    { 1, "TS", 1, "Tech Solutions s.r.o.", null, 0 },
                    { 2, "DEV", 2, "Software Development", 1, 1 },
                    { 3, "OPS", 3, "IT Operations", 1, 1 },
                    { 4, "ERP", 4, "ERP System", 2, 2 },
                    { 5, "CLOUD", 5, "Cloud Migration", 3, 2 },
                    { 6, "BE", 6, "Backend Team", 4, 3 },
                    { 7, "DO", 3, "DevOps Team", 5, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrgUnits",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
