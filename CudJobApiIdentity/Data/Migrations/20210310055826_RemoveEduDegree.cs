using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class RemoveEduDegree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationDegree");

            migrationBuilder.AddColumn<string>(
                name: "Certificate_Diploma",
                table: "StudentEducation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Completion_Date",
                table: "StudentEducation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Completion_Percent",
                table: "StudentEducation",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrrentSalary",
                table: "StudentEducation",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "StudentEducation",
                type: "Nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "StudentEducation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start_Date",
                table: "StudentEducation",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certificate_Diploma",
                table: "StudentEducation");

            migrationBuilder.DropColumn(
                name: "Completion_Date",
                table: "StudentEducation");

            migrationBuilder.DropColumn(
                name: "Completion_Percent",
                table: "StudentEducation");

            migrationBuilder.DropColumn(
                name: "CurrrentSalary",
                table: "StudentEducation");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "StudentEducation");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "StudentEducation");

            migrationBuilder.DropColumn(
                name: "Start_Date",
                table: "StudentEducation");

            migrationBuilder.CreateTable(
                name: "EducationDegree",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Certificate_Diploma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Completion_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completion_Percent = table.Column<byte>(type: "tinyint", nullable: true),
                    CreateddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrrentSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Degree = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDegree", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EducationDegree_StudentEducation_EducationId",
                        column: x => x.EducationId,
                        principalTable: "StudentEducation",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationDegree_EducationId",
                table: "EducationDegree",
                column: "EducationId");
        }
    }
}
