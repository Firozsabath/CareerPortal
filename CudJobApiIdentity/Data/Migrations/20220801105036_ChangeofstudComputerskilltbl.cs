using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class ChangeofstudComputerskilltbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentComputerSkills_Computerskills_ComputerSkillID",
                table: "StudentComputerSkills");

            migrationBuilder.AlterColumn<int>(
                name: "ComputerSkillID",
                table: "StudentComputerSkills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ComputerSkill",
                table: "StudentComputerSkills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentComputerSkills_Computerskills_ComputerSkillID",
                table: "StudentComputerSkills",
                column: "ComputerSkillID",
                principalTable: "Computerskills",
                principalColumn: "CskillID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentComputerSkills_Computerskills_ComputerSkillID",
                table: "StudentComputerSkills");

            migrationBuilder.DropColumn(
                name: "ComputerSkill",
                table: "StudentComputerSkills");

            migrationBuilder.AlterColumn<int>(
                name: "ComputerSkillID",
                table: "StudentComputerSkills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentComputerSkills_Computerskills_ComputerSkillID",
                table: "StudentComputerSkills",
                column: "ComputerSkillID",
                principalTable: "Computerskills",
                principalColumn: "CskillID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
