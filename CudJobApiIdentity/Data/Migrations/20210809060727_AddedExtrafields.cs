using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class AddedExtrafields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Credihours",
                table: "Students",
                type: "Nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Intern",
                table: "Students",
                type: "Nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Nationality",
                table: "Address",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_Nationality",
                table: "Address",
                column: "Nationality");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_CountryCode_Nationality",
                table: "Address",
                column: "Nationality",
                principalTable: "CountryCode",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_CountryCode_Nationality",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_Nationality",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Credihours",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Intern",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Address");
        }
    }
}
