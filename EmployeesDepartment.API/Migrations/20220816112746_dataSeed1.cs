using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesDepartment.API.Migrations
{
    public partial class dataSeed1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { 1, "C5.302", "Frontend" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { 2, null, "Backend" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { 3, null, "Quality Control" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "Job", "Name", "Salary" },
                values: new object[] { 1, 2, "Intern", "Ahmed Amr", 3000.0 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "Job", "Name", "Salary" },
                values: new object[] { 2, 2, "Project Lead", "Ahmed Mostafa", 0.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
