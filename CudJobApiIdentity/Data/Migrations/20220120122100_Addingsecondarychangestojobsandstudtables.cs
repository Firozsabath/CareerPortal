using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class Addingsecondarychangestojobsandstudtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_Job_durations_durationID",
                table: "JobModel");

            migrationBuilder.AddColumn<float>(
                name: "current",
                table: "StudentExperience",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "current",
                table: "Studenteducation",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<int>(
                name: "durationID",
                table: "JobModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Jobduration",
                table: "JobModel",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_Job_durations_durationID",
                table: "JobModel",
                column: "durationID",
                principalTable: "Job_durations",
                principalColumn: "durationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_Job_durations_durationID",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "current",
                table: "StudentExperience");

            migrationBuilder.DropColumn(
                name: "current",
                table: "Studenteducation");

            migrationBuilder.DropColumn(
                name: "Jobduration",
                table: "JobModel");

            migrationBuilder.AlterColumn<int>(
                name: "durationID",
                table: "JobModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_Job_durations_durationID",
                table: "JobModel",
                column: "durationID",
                principalTable: "Job_durations",
                principalColumn: "durationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
