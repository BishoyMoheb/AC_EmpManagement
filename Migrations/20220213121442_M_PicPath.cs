using Microsoft.EntityFrameworkCore.Migrations;

namespace AC_EmpManagement.Migrations
{
    public partial class M_PicPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicPath",
                table: "DbS_Emps",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DbS_Emps",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "pishooo@parthy.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicPath",
                table: "DbS_Emps");

            migrationBuilder.UpdateData(
                table: "DbS_Emps",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "Pishooo@parthy.com");
        }
    }
}
