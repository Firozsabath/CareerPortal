using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class AddingApprovaltojob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentEducation_Students_StudentID",
                table: "StudentEducation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEducation",
                table: "StudentEducation");

            migrationBuilder.RenameTable(
                name: "StudentEducation",
                newName: "Studenteducation");

            migrationBuilder.RenameIndex(
                name: "IX_StudentEducation_StudentID",
                table: "Studenteducation",
                newName: "IX_Studenteducation_StudentID");

            migrationBuilder.AddColumn<string>(
                name: "Objective",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "JobModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Studenteducation",
                table: "Studenteducation",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studenteducation_Students_StudentID",
                table: "Studenteducation",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studenteducation_Students_StudentID",
                table: "Studenteducation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Studenteducation",
                table: "Studenteducation");

            migrationBuilder.DropColumn(
                name: "Objective",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "JobModel");

            migrationBuilder.RenameTable(
                name: "Studenteducation",
                newName: "StudentEducation");

            migrationBuilder.RenameIndex(
                name: "IX_Studenteducation_StudentID",
                table: "StudentEducation",
                newName: "IX_StudentEducation_StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEducation",
                table: "StudentEducation",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEducation_Students_StudentID",
                table: "StudentEducation",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
