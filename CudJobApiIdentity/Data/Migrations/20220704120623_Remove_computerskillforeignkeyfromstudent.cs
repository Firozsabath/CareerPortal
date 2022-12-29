using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class Remove_computerskillforeignkeyfromstudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CskillID",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_CskillID",
                table: "Students",
                column: "CskillID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students",
                column: "CskillID",
                principalTable: "Computerskills",
                principalColumn: "CskillID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
