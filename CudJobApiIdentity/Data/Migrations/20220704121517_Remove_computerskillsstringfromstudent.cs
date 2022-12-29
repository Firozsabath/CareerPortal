using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class Remove_computerskillsstringfromstudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentComputerSkills_Students_StudentSeekersStudentID",
                table: "StudentComputerSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHardSkills_Students_StudentSeekersStudentID",
                table: "StudentHardSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSoftSkills_Students_StudentSeekersStudentID",
                table: "StudentSoftSkills");

            migrationBuilder.DropIndex(
                name: "IX_StudentSoftSkills_StudentSeekersStudentID",
                table: "StudentSoftSkills");

            migrationBuilder.DropIndex(
                name: "IX_StudentHardSkills_StudentSeekersStudentID",
                table: "StudentHardSkills");

            migrationBuilder.DropIndex(
                name: "IX_StudentComputerSkills_StudentSeekersStudentID",
                table: "StudentComputerSkills");

            migrationBuilder.DropColumn(
                name: "StudentSeekersStudentID",
                table: "StudentSoftSkills");

            migrationBuilder.DropColumn(
                name: "ComputerSkills",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentSeekersStudentID",
                table: "StudentHardSkills");

            migrationBuilder.DropColumn(
                name: "StudentSeekersStudentID",
                table: "StudentComputerSkills");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSoftSkills_StudentID",
                table: "StudentSoftSkills",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHardSkills_StudentID",
                table: "StudentHardSkills",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentComputerSkills_StudentID",
                table: "StudentComputerSkills",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentComputerSkills_Students_StudentID",
                table: "StudentComputerSkills",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHardSkills_Students_StudentID",
                table: "StudentHardSkills",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSoftSkills_Students_StudentID",
                table: "StudentSoftSkills",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentComputerSkills_Students_StudentID",
                table: "StudentComputerSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentHardSkills_Students_StudentID",
                table: "StudentHardSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSoftSkills_Students_StudentID",
                table: "StudentSoftSkills");

            migrationBuilder.DropIndex(
                name: "IX_StudentSoftSkills_StudentID",
                table: "StudentSoftSkills");

            migrationBuilder.DropIndex(
                name: "IX_StudentHardSkills_StudentID",
                table: "StudentHardSkills");

            migrationBuilder.DropIndex(
                name: "IX_StudentComputerSkills_StudentID",
                table: "StudentComputerSkills");

            migrationBuilder.AddColumn<int>(
                name: "StudentSeekersStudentID",
                table: "StudentSoftSkills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComputerSkills",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentSeekersStudentID",
                table: "StudentHardSkills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentSeekersStudentID",
                table: "StudentComputerSkills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSoftSkills_StudentSeekersStudentID",
                table: "StudentSoftSkills",
                column: "StudentSeekersStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHardSkills_StudentSeekersStudentID",
                table: "StudentHardSkills",
                column: "StudentSeekersStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentComputerSkills_StudentSeekersStudentID",
                table: "StudentComputerSkills",
                column: "StudentSeekersStudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentComputerSkills_Students_StudentSeekersStudentID",
                table: "StudentComputerSkills",
                column: "StudentSeekersStudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentHardSkills_Students_StudentSeekersStudentID",
                table: "StudentHardSkills",
                column: "StudentSeekersStudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSoftSkills_Students_StudentSeekersStudentID",
                table: "StudentSoftSkills",
                column: "StudentSeekersStudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
