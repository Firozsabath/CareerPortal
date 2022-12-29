using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class AddingSkillsandLanguagestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "level",
                table: "Languages");

            migrationBuilder.AddColumn<int>(
                name: "CskillID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HskillID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SskillID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LanguageID",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LevelID",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Shortheading",
                table: "JobModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Computerskills",
                columns: table => new
                {
                    CskillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftSkills = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computerskills", x => x.CskillID);
                });

            migrationBuilder.CreateTable(
                name: "Hardskills",
                columns: table => new
                {
                    HskillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HardSkills = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardskills", x => x.HskillID);
                });

            migrationBuilder.CreateTable(
                name: "LanguageLevels",
                columns: table => new
                {
                    LevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Levels = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageLevels", x => x.LevelID);
                });

            migrationBuilder.CreateTable(
                name: "LanguageNames",
                columns: table => new
                {
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageNames", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "Softskills",
                columns: table => new
                {
                    SskillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftSkills = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softskills", x => x.SskillID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CskillID",
                table: "Students",
                column: "CskillID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_HskillID",
                table: "Students",
                column: "HskillID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SskillID",
                table: "Students",
                column: "SskillID");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LanguageID",
                table: "Languages",
                column: "LanguageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LevelID",
                table: "Languages",
                column: "LevelID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_LanguageLevels_LevelID",
                table: "Languages",
                column: "LevelID",
                principalTable: "LanguageLevels",
                principalColumn: "LevelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_LanguageNames_LanguageID",
                table: "Languages",
                column: "LanguageID",
                principalTable: "LanguageNames",
                principalColumn: "LanguageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students",
                column: "CskillID",
                principalTable: "Computerskills",
                principalColumn: "CskillID",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LanguageLevels_LevelID",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LanguageNames_LanguageID",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Computerskills_CskillID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Hardskills_HskillID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Softskills_SskillID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Computerskills");

            migrationBuilder.DropTable(
                name: "Hardskills");

            migrationBuilder.DropTable(
                name: "LanguageLevels");

            migrationBuilder.DropTable(
                name: "LanguageNames");

            migrationBuilder.DropTable(
                name: "Softskills");

            migrationBuilder.DropIndex(
                name: "IX_Students_CskillID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_HskillID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SskillID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LanguageID",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LevelID",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "CskillID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HskillID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SskillID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LanguageID",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "LevelID",
                table: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Languages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "level",
                table: "Languages",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Shortheading",
                table: "JobModel",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
