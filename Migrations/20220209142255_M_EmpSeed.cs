using Microsoft.EntityFrameworkCore.Migrations;

namespace AC_EmpManagement.Migrations
{
    public partial class M_EmpSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DbS_Emps",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 1, 1, "Pishooo@parthy.com", "Pishooo" });

            migrationBuilder.InsertData(
                table: "DbS_Emps",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 2, 2, "qalby@parthy.com", "Qalby" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DbS_Emps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DbS_Emps",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
