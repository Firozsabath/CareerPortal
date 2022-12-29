using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class StatusesNotesUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CudStudentID",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NotesID",
                table: "JobModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusIDs",
                table: "JobModel",
                type: "int",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Do_you_cover_incaseof_work_accidents",
            //    table: "CompanyOptional",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotesID",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusIDs",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusIDs = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyProperty = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusIDs);
                });

            migrationBuilder.CreateTable(
                name: "StatusesNotes",
                columns: table => new
                {
                    NotesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusesNotes", x => x.NotesID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_NotesID",
                table: "JobModel",
                column: "NotesID");

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_StatusIDs",
                table: "JobModel",
                column: "StatusIDs");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_NotesID",
                table: "Companies",
                column: "NotesID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_StatusIDs",
                table: "Companies",
                column: "StatusIDs");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Statuses_StatusIDs",
                table: "Companies",
                column: "StatusIDs",
                principalTable: "Statuses",
                principalColumn: "StatusIDs",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_StatusesNotes_NotesID",
                table: "Companies",
                column: "NotesID",
                principalTable: "StatusesNotes",
                principalColumn: "NotesID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_Statuses_StatusIDs",
                table: "JobModel",
                column: "StatusIDs",
                principalTable: "Statuses",
                principalColumn: "StatusIDs",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobModel_StatusesNotes_NotesID",
                table: "JobModel",
                column: "NotesID",
                principalTable: "StatusesNotes",
                principalColumn: "NotesID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Statuses_StatusIDs",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_StatusesNotes_NotesID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_Statuses_StatusIDs",
                table: "JobModel");

            migrationBuilder.DropForeignKey(
                name: "FK_JobModel_StatusesNotes_NotesID",
                table: "JobModel");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "StatusesNotes");

            migrationBuilder.DropIndex(
                name: "IX_JobModel_NotesID",
                table: "JobModel");

            migrationBuilder.DropIndex(
                name: "IX_JobModel_StatusIDs",
                table: "JobModel");

            migrationBuilder.DropIndex(
                name: "IX_Companies_NotesID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_StatusIDs",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "NotesID",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "StatusIDs",
                table: "JobModel");

            migrationBuilder.DropColumn(
                name: "Do_you_cover_incaseof_work_accidents",
                table: "CompanyOptional");

            migrationBuilder.DropColumn(
                name: "NotesID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StatusIDs",
                table: "Companies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CudStudentID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
