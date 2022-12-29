using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class ChangedJobOptionstblconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOptions_JobModel_JobsId",
                table: "JobOptions");

            migrationBuilder.DropIndex(
                name: "IX_JobOptions_JobsId",
                table: "JobOptions");

            migrationBuilder.DropColumn(
                name: "JobsId",
                table: "JobOptions");

            migrationBuilder.AddColumn<int>(
                name: "JobOptionsJoboptionID",
                table: "JobModel",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_JobOptions_JobOptionsJoboptionID",
                table: "JobModel");

            migrationBuilder.DropIndex(
                name: "IX_JobModel_JobOptionsJoboptionID",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "JobOptionsJoboptionID",
                table: "JobModel");

            migrationBuilder.AddColumn<int>(
                name: "JobsId",
                table: "JobOptions",
                type: "int",
                nullable: true);

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
        }
    }
}
