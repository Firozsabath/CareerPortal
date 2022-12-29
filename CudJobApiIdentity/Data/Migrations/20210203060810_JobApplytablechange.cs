using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class JobApplytablechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Students",
                type: "Nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resumepath",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisaStatus",
                table: "Students",
                type: "Nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profileImgpath",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Applytype",
                table: "JobModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalUrl",
                table: "JobModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "JobModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Certificatepath",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profileImgpath",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppliedJobs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedJobs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AppliedJobs_JobModel_jobID",
                        column: x => x.jobID,
                        principalTable: "JobModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedJobs_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    JobCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.JobCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_JobCategoryId",
                table: "JobModel",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedJobs_jobID",
                table: "AppliedJobs",
                column: "jobID");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedJobs_StudentID",
                table: "AppliedJobs",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel",
                column: "JobCategoryId",
                principalTable: "JobCategory",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobCategory_JobCategoryId",
                table: "JobModel");

            migrationBuilder.DropTable(
                name: "AppliedJobs");

            migrationBuilder.DropTable(
                name: "JobCategory");

            migrationBuilder.DropIndex(
                name: "IX_JobModel_JobCategoryId",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Resumepath",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "VisaStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "profileImgpath",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Applytype",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "ExternalUrl",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "Certificatepath",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "profileImgpath",
                table: "Companies");
        }
    }
}
