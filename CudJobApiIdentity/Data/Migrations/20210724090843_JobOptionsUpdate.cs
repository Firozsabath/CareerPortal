using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class JobOptionsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobOptions_JobOptionsJoboptionID",
                table: "JobModel");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOptions_Students_JobID",
                table: "JobOptions");

            migrationBuilder.DropIndex(
                name: "IX_JobOptions_JobID",
                table: "JobOptions");

            migrationBuilder.DropIndex(
                name: "IX_JobModel_JobOptionsJoboptionID",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "JobID",
                table: "JobOptions");

            migrationBuilder.DropColumn(
                name: "JobOptionsJoboptionID",
                table: "JobModel");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "JobOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOptions_ID",
                table: "JobOptions",
                column: "ID",
                unique: true,
                filter: "[ID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOptions_JobModel_ID",
                table: "JobOptions",
                column: "ID",
                principalTable: "JobModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOptions_JobModel_ID",
                table: "JobOptions");

            migrationBuilder.DropIndex(
                name: "IX_JobOptions_ID",
                table: "JobOptions");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "JobOptions");

            migrationBuilder.AddColumn<int>(
                name: "JobID",
                table: "JobOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobOptionsJoboptionID",
                table: "JobModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOptions_JobID",
                table: "JobOptions",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_JobOptionsJoboptionID",
                table: "JobModel",
                column: "JobOptionsJoboptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_JobOptions_JobOptionsJoboptionID",
                table: "JobModel",
                column: "JobOptionsJoboptionID",
                principalTable: "JobOptions",
                principalColumn: "JoboptionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOptions_Students_JobID",
                table: "JobOptions",
                column: "JobID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
