using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class AddingCompanyoptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailID2",
                table: "Students",
                type: "Nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber2",
                table: "Students",
                type: "Nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyStrength",
                table: "Companies",
                type: "Nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthMeasures",
                table: "Companies",
                type: "Nvarchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailID2",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MobileNumber2",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CompanyStrength",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HealthMeasures",
                table: "Companies");
        }
    }
}
