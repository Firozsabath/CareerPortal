using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class RemovingforeignkeylinkinStudCompSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentComputerSkills_Computerskills_ComputerSkillID",
                table: "StudentComputerSkills");

            migrationBuilder.DropIndex(
                name: "IX_StudentComputerSkills_ComputerSkillID",
                table: "StudentComputerSkills");

            migrationBuilder.DropColumn(
                name: "ComputerSkillID",
                table: "StudentComputerSkills");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComputerSkillID",
                table: "StudentComputerSkills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentComputerSkills_ComputerSkillID",
                table: "StudentComputerSkills",
                column: "ComputerSkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentComputerSkills_Computerskills_ComputerSkillID",
                table: "StudentComputerSkills",
                column: "ComputerSkillID",
                principalTable: "Computerskills",
                principalColumn: "CskillID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
