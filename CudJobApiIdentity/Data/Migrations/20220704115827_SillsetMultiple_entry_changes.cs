using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class SillsetMultiple_entry_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Hardskills_HskillID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Softskills_SskillID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_HskillID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SskillID",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "HardskillsHskillID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentComputerSkills",
                columns: table => new
                {
                    StudentCsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ComputerSkillID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StudentSeekersStudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentComputerSkills", x => x.StudentCsID);
                    table.ForeignKey(
                        name: "FK_StudentComputerSkills_Computerskills_ComputerSkillID",
                        column: x => x.ComputerSkillID,
                        principalTable: "Computerskills",
                        principalColumn: "CskillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentComputerSkills_Students_StudentSeekersStudentID",
                        column: x => x.StudentSeekersStudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentHardSkills",
                columns: table => new
                {
                    StudentHsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    HardSkillID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StudentSeekersStudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHardSkills", x => x.StudentHsID);
                    table.ForeignKey(
                        name: "FK_StudentHardSkills_Hardskills_HardSkillID",
                        column: x => x.HardSkillID,
                        principalTable: "Hardskills",
                        principalColumn: "HskillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentHardSkills_Students_StudentSeekersStudentID",
                        column: x => x.StudentSeekersStudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentSoftSkills",
                columns: table => new
                {
                    StudentSsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SoftSkillID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StudentSeekersStudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSoftSkills", x => x.StudentSsID);
                    table.ForeignKey(
                        name: "FK_StudentSoftSkills_Softskills_SoftSkillID",
                        column: x => x.SoftSkillID,
                        principalTable: "Softskills",
                        principalColumn: "SskillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSoftSkills_Students_StudentSeekersStudentID",
                        column: x => x.StudentSeekersStudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_HardskillsHskillID",
                table: "Students",
                column: "HardskillsHskillID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentComputerSkills_ComputerSkillID",
                table: "StudentComputerSkills",
                column: "ComputerSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentComputerSkills_StudentSeekersStudentID",
                table: "StudentComputerSkills",
                column: "StudentSeekersStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHardSkills_HardSkillID",
                table: "StudentHardSkills",
                column: "HardSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHardSkills_StudentSeekersStudentID",
                table: "StudentHardSkills",
                column: "StudentSeekersStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSoftSkills_SoftSkillID",
                table: "StudentSoftSkills",
                column: "SoftSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSoftSkills_StudentSeekersStudentID",
                table: "StudentSoftSkills",
                column: "StudentSeekersStudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Hardskills_HardskillsHskillID",
                table: "Students",
                column: "HardskillsHskillID",
                principalTable: "Hardskills",
                principalColumn: "HskillID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Hardskills_HardskillsHskillID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentComputerSkills");

            migrationBuilder.DropTable(
                name: "StudentHardSkills");

            migrationBuilder.DropTable(
                name: "StudentSoftSkills");

            migrationBuilder.DropIndex(
                name: "IX_Students_HardskillsHskillID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HardskillsHskillID",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_Students_HskillID",
                table: "Students",
                column: "HskillID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SskillID",
                table: "Students",
                column: "SskillID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Hardskills_HskillID",
                table: "Students",
                column: "HskillID",
                principalTable: "Hardskills",
                principalColumn: "HskillID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Softskills_SskillID",
                table: "Students",
                column: "SskillID",
                principalTable: "Softskills",
                principalColumn: "SskillID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
