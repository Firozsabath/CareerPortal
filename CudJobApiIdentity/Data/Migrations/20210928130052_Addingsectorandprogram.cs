using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class Addingsectorandprogram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectorID",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CompanySectors",
                columns: table => new
                {
                    SectorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySectors", x => x.SectorID);
                });

            migrationBuilder.CreateTable(
                name: "programs",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programs", x => x.ProgramID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramID",
                table: "Students",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SectorID",
                table: "Companies",
                column: "SectorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanySectors_SectorID",
                table: "Companies",
                column: "SectorID",
                principalTable: "CompanySectors",
                principalColumn: "SectorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_programs_ProgramID",
                table: "Students",
                column: "ProgramID",
                principalTable: "programs",
                principalColumn: "ProgramID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanySectors_SectorID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_programs_ProgramID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CompanySectors");

            migrationBuilder.DropTable(
                name: "programs");

            migrationBuilder.DropIndex(
                name: "IX_Students_ProgramID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Companies_SectorID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProgramID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SectorID",
                table: "Companies");
        }
    }
}
