using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class AddingWorkAvailabilty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysPerWeek",
                table: "StudentWorkAvailability");

            migrationBuilder.DropColumn(
                name: "HoursPerWeek",
                table: "StudentWorkAvailability");

            migrationBuilder.AddColumn<int>(
                name: "DaysPerWeekID",
                table: "StudentWorkAvailability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoursPerWeekID",
                table: "StudentWorkAvailability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DaysPerWeekOptions",
                columns: table => new
                {
                    DaysPerWeekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysPerWeekOptions", x => x.DaysPerWeekID);
                });

            migrationBuilder.CreateTable(
                name: "HoursPerWeekOptions",
                columns: table => new
                {
                    HoursPerWeekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursPerWeekOptions", x => x.HoursPerWeekID);
                });

            migrationBuilder.CreateTable(
                name: "JobsWorkAvailability",
                columns: table => new
                {
                    AvailabilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    DaysPerWeekID = table.Column<int>(type: "int", nullable: false),
                    HoursPerWeekID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobsWorkAvailability", x => x.AvailabilityID);
                    table.ForeignKey(
                        name: "FK_JobsWorkAvailability_DaysPerWeekOptions_DaysPerWeekID",
                        column: x => x.DaysPerWeekID,
                        principalTable: "DaysPerWeekOptions",
                        principalColumn: "DaysPerWeekID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobsWorkAvailability_HoursPerWeekOptions_HoursPerWeekID",
                        column: x => x.HoursPerWeekID,
                        principalTable: "HoursPerWeekOptions",
                        principalColumn: "HoursPerWeekID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobsWorkAvailability_JobModel_JobID",
                        column: x => x.JobID,
                        principalTable: "JobModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorkAvailability_DaysPerWeekID",
                table: "StudentWorkAvailability",
                column: "DaysPerWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentWorkAvailability_HoursPerWeekID",
                table: "StudentWorkAvailability",
                column: "HoursPerWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_JobsWorkAvailability_DaysPerWeekID",
                table: "JobsWorkAvailability",
                column: "DaysPerWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_JobsWorkAvailability_HoursPerWeekID",
                table: "JobsWorkAvailability",
                column: "HoursPerWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_JobsWorkAvailability_JobID",
                table: "JobsWorkAvailability",
                column: "JobID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentWorkAvailability_DaysPerWeekOptions_DaysPerWeekID",
                table: "StudentWorkAvailability",
                column: "DaysPerWeekID",
                principalTable: "DaysPerWeekOptions",
                principalColumn: "DaysPerWeekID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentWorkAvailability_HoursPerWeekOptions_HoursPerWeekID",
                table: "StudentWorkAvailability",
                column: "HoursPerWeekID",
                principalTable: "HoursPerWeekOptions",
                principalColumn: "HoursPerWeekID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentWorkAvailability_DaysPerWeekOptions_DaysPerWeekID",
                table: "StudentWorkAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentWorkAvailability_HoursPerWeekOptions_HoursPerWeekID",
                table: "StudentWorkAvailability");

            migrationBuilder.DropTable(
                name: "JobsWorkAvailability");

            migrationBuilder.DropTable(
                name: "DaysPerWeekOptions");

            migrationBuilder.DropTable(
                name: "HoursPerWeekOptions");

            migrationBuilder.DropIndex(
                name: "IX_StudentWorkAvailability_DaysPerWeekID",
                table: "StudentWorkAvailability");

            migrationBuilder.DropIndex(
                name: "IX_StudentWorkAvailability_HoursPerWeekID",
                table: "StudentWorkAvailability");

            migrationBuilder.DropColumn(
                name: "DaysPerWeekID",
                table: "StudentWorkAvailability");

            migrationBuilder.DropColumn(
                name: "HoursPerWeekID",
                table: "StudentWorkAvailability");

            migrationBuilder.AddColumn<string>(
                name: "DaysPerWeek",
                table: "StudentWorkAvailability",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoursPerWeek",
                table: "StudentWorkAvailability",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
