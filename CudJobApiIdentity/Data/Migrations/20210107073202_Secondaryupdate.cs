using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class Secondaryupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddressCollection_StudentEducation_StudentID",
                table: "StudentAddressCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddressCollection_Students_StudentSeekersStudentID",
                table: "StudentAddressCollection");

            migrationBuilder.DropIndex(
                name: "IX_StudentAddressCollection_StudentSeekersStudentID",
                table: "StudentAddressCollection");

            migrationBuilder.DropColumn(
                name: "StudentSeekersStudentID",
                table: "StudentAddressCollection");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddressCollection_Students_StudentID",
                table: "StudentAddressCollection",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddressCollection_Students_StudentID",
                table: "StudentAddressCollection");

            migrationBuilder.AddColumn<int>(
                name: "StudentSeekersStudentID",
                table: "StudentAddressCollection",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddressCollection_StudentSeekersStudentID",
                table: "StudentAddressCollection",
                column: "StudentSeekersStudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddressCollection_StudentEducation_StudentID",
                table: "StudentAddressCollection",
                column: "StudentID",
                principalTable: "StudentEducation",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddressCollection_Students_StudentSeekersStudentID",
                table: "StudentAddressCollection",
                column: "StudentSeekersStudentID",
                principalTable: "Students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
