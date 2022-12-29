using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class CampnayCategoryandCountryupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "CompanyTypeID",
                table: "Companies",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Address",
                newName: "CountryID");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CompanyCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "CountryCode",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCode", x => x.CountryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CategoryID",
                table: "Companies",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryID",
                table: "Address",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_CountryCode_CountryID",
                table: "Address",
                column: "CountryID",
                principalTable: "CountryCode",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyCategory_CategoryID",
                table: "Companies",
                column: "CategoryID",
                principalTable: "CompanyCategory",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_CountryCode_CountryID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyCategory_CategoryID",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyCategory");

            migrationBuilder.DropTable(
                name: "CountryCode");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CategoryID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Address_CountryID",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Companies",
                newName: "CompanyTypeID");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Address",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
