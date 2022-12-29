using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class AddingJobExperienceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel");

            migrationBuilder.AlterColumn<int>(
                name: "JobCategoryId",
                table: "JobModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceID",
                table: "JobModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobExperiences",
                columns: table => new
                {
                    ExperienceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExperiences", x => x.ExperienceID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_ExperienceID",
                table: "JobModel",
                column: "ExperienceID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel");

            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobExperiences_ExperienceID",
                table: "JobModel");

            migrationBuilder.DropTable(
                name: "JobExperiences");

            migrationBuilder.DropIndex(
                name: "IX_JobModel_ExperienceID",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "ExperienceID",
                table: "JobModel");

            migrationBuilder.AlterColumn<int>(
                name: "JobCategoryId",
                table: "JobModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
