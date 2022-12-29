using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class DbChangesafterDemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel");

            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobExperiences_ExperienceID",
                table: "JobModel");

            migrationBuilder.AddColumn<string>(
                name: "Porgram",
                table: "Students",
                type: "Nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "StudentExperience",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Company_website",
                table: "StudentExperience",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "StudentExperience",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobCategoryId",
                table: "JobModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExperienceID",
                table: "JobModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppliedJobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "AppliedJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "Nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Studentportfolio",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    portfolio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studentportfolio", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_Studentportfolio_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentWorkAvailability",
                columns: table => new
                {
                    AvailabilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    DaysPerWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoursPerWeek = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentWorkAvailability", x => x.AvailabilityID);
                    table.ForeignKey(
                        name: "FK_StudentWorkAvailability_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentExperience_CategoryID",
                table: "StudentExperience",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedJobs_StatusID",
                table: "AppliedJobs",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Studentportfolio_StudentID",
                table: "Studentportfolio",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorkAvailability_StudentID",
                table: "StudentWorkAvailability",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppliedJobs_JobStatuses_StatusID",
                table: "AppliedJobs",
                column: "StatusID",
                principalTable: "JobStatuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobExperiences_ExperienceID",
                table: "JobModel",
                column: "ExperienceID",
                principalTable: "JobExperiences",
                principalColumn: "ExperienceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExperience_CompanyCategory_CategoryID",
                table: "StudentExperience",
                column: "CategoryID",
                principalTable: "CompanyCategory",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppliedJobs_JobStatuses_StatusID",
                table: "AppliedJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel");

            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobExperiences_ExperienceID",
                table: "JobModel");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExperience_CompanyCategory_CategoryID",
                table: "StudentExperience");

            migrationBuilder.DropTable(
                name: "JobStatuses");

            migrationBuilder.DropTable(
                name: "Studentportfolio");

            migrationBuilder.DropTable(
                name: "StudentWorkAvailability");

            migrationBuilder.DropIndex(
                name: "IX_StudentExperience_CategoryID",
                table: "StudentExperience");

            migrationBuilder.DropIndex(
                name: "IX_AppliedJobs_StatusID",
                table: "AppliedJobs");

            migrationBuilder.DropColumn(
                name: "Porgram",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "StudentExperience");

            migrationBuilder.DropColumn(
                name: "Company_website",
                table: "StudentExperience");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "StudentExperience");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppliedJobs");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "AppliedJobs");

            migrationBuilder.AlterColumn<int>(
                name: "JobCategoryId",
                table: "JobModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExperienceID",
                table: "JobModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobExperiences_ExperienceID",
                table: "JobModel",
                column: "ExperienceID",
                principalTable: "JobExperiences",
                principalColumn: "ExperienceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
