using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class ChangedJobOptionstbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobOptions_JoboptionID",
                table: "JobModel");

            migrationBuilder.DropIndex(
                name: "IX_JobModel_JoboptionID",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "JoboptionID",
                table: "JobModel");

            migrationBuilder.AddColumn<int>(
                name: "JobID",
                table: "JobOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobsId",
                table: "JobOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOptions_JobID",
                table: "JobOptions",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobOptions_JobsId",
                table: "JobOptions",
                column: "JobsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOptions_JobModel_JobsId",
                table: "JobOptions",
                column: "JobsId",
                principalTable: "JobModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOptions_Students_JobID",
                table: "JobOptions",
                column: "JobID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOptions_JobModel_JobsId",
                table: "JobOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOptions_Students_JobID",
                table: "JobOptions");

            migrationBuilder.DropIndex(
                name: "IX_JobOptions_JobID",
                table: "JobOptions");

            migrationBuilder.DropIndex(
                name: "IX_JobOptions_JobsId",
                table: "JobOptions");

            migrationBuilder.DropColumn(
                name: "JobID",
                table: "JobOptions");

            migrationBuilder.DropColumn(
                name: "JobsId",
                table: "JobOptions");

            migrationBuilder.AddColumn<int>(
                name: "JoboptionID",
                table: "JobModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_JoboptionID",
                table: "JobModel",
                column: "JoboptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobOptions_JoboptionID",
                table: "JobModel",
                column: "JoboptionID",
                principalTable: "JobOptions",
                principalColumn: "JoboptionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
