using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class updatevolunteertablechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students");

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdate",
                table: "StudentWorkAvailability",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedate",
                table: "StudentWorkAvailability",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CskillID",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ComputerSkills",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdate",
                table: "Studentportfolio",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedate",
                table: "Studentportfolio",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "End_Date",
                table: "StudentExperience",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "current",
                table: "Studenteducation",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "Studenteducation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateddDate",
                table: "Studenteducation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedate",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "current",
                table: "Projects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdate",
                table: "Memberships",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedate",
                table: "Memberships",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "current",
                table: "Memberships",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdate",
                table: "Languages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedate",
                table: "Languages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdate",
                table: "JobsWorkAvailability",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedate",
                table: "JobsWorkAvailability",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdate",
                table: "Awards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedate",
                table: "Awards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VolunteerExperience",
                columns: table => new
                {
                    VexpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Company_Name = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: true),
                    Job_Title = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: true),
                    Job_Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    current = table.Column<bool>(type: "bit", nullable: true),
                    Start_Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Createdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updatedate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerExperience", x => x.VexpId);
                    table.ForeignKey(
                        name: "FK_VolunteerExperience_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerExperience_StudentID",
                table: "VolunteerExperience",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students",
                column: "CskillID",
                principalTable: "Computerskills",
                principalColumn: "CskillID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "VolunteerExperience");

            migrationBuilder.DropColumn(
                name: "Createdate",
                table: "StudentWorkAvailability");

            migrationBuilder.DropColumn(
                name: "Updatedate",
                table: "StudentWorkAvailability");

            migrationBuilder.DropColumn(
                name: "ComputerSkills",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Createdate",
                table: "Studentportfolio");

            migrationBuilder.DropColumn(
                name: "Updatedate",
                table: "Studentportfolio");

            migrationBuilder.DropColumn(
                name: "Createdate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Updatedate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "current",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Createdate",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "Updatedate",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "current",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "Createdate",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Updatedate",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Createdate",
                table: "JobsWorkAvailability");

            migrationBuilder.DropColumn(
                name: "Updatedate",
                table: "JobsWorkAvailability");

            migrationBuilder.DropColumn(
                name: "Createdate",
                table: "Awards");

            migrationBuilder.DropColumn(
                name: "Updatedate",
                table: "Awards");

            migrationBuilder.AlterColumn<int>(
                name: "CskillID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "End_Date",
                table: "StudentExperience",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "current",
                table: "Studenteducation",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "Studenteducation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateddDate",
                table: "Studenteducation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students",
                column: "CskillID",
                principalTable: "Computerskills",
                principalColumn: "CskillID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
