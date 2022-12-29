using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class AddingCompanyoptionalagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyOptional",
                columns: table => new
                {
                    OptionalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    Fulltimeoffer = table.Column<bool>(type: "bit", nullable: false),
                    FlexibleHours_forFulltime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workingfromoffice_forFulltime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parttimeoffer = table.Column<bool>(type: "bit", nullable: false),
                    FlexibleHours_forParttime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workingfromoffice_forParttime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Internshipoffer = table.Column<bool>(type: "bit", nullable: false),
                    FlexibleHours_forInternship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workingfromoffice_forInternship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidInternship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Onemonth_Internship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Morethan_Onemonth_Internship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Partcipate_CUDAnnualcareerfair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Partcipateorsponsor_CUDEvents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workshops_tostudent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CUD_Incubator_Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Share_ContactDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOptional", x => x.OptionalID);
                    table.ForeignKey(
                        name: "FK_CompanyOptional_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOptional_CompanyID",
                table: "CompanyOptional",
                column: "CompanyID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyOptional");
        }
    }
}
