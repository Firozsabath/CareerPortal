using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class smallfixesforforeignkeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LanguageLevels_LevelID",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LanguageNames_LanguageID",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LanguageID",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LevelID",
                table: "Languages");

            migrationBuilder.AlterColumn<int>(
                name: "LevelID",
                table: "Languages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "Languages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LanguageID",
                table: "Languages",
                column: "LanguageID",
                unique: true,
                filter: "[LanguageID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LevelID",
                table: "Languages",
                column: "LevelID",
                unique: true,
                filter: "[LevelID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_LanguageLevels_LevelID",
                table: "Languages",
                column: "LevelID",
                principalTable: "LanguageLevels",
                principalColumn: "LevelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_LanguageNames_LanguageID",
                table: "Languages",
                column: "LanguageID",
                principalTable: "LanguageNames",
                principalColumn: "LanguageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LanguageLevels_LevelID",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LanguageNames_LanguageID",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LanguageID",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LevelID",
                table: "Languages");

            migrationBuilder.AlterColumn<int>(
                name: "LevelID",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
        }
    }
}
