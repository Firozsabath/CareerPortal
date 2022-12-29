using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CudJobApiIdentity.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTypeID = table.Column<int>(type: "int", nullable: false),
                    Address1 = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    City = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    State = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    Country = table.Column<int>(type: "int", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    CreateddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Website = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyTypeID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Addess = table.Column<string>(type: "Nvarchar(200)", nullable: true),
                    City = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "Nvarchar(10)", nullable: true),
                    MobileNumber = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    EmailID = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    Password = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    CudStudentID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAddressCollection",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAddressCollection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyAddressCollection_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyAddressCollection_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContacts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    EmailID = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    Contact_No = table.Column<string>(type: "Nvarchar(50)", nullable: true),
                    CreateddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyContacts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReguiredSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedSalary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastApplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobModel_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentEducation",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Institution = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    CreateddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEducation", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_StudentEducation_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExperience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Company_Name = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Country_Code = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Job_Title = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    Job_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExperience_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationDegree",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree = table.Column<string>(type: "Nvarchar(100)", nullable: true),
                    CurrrentSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Certificate_Diploma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completion_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Completion_Percent = table.Column<byte>(type: "tinyint", nullable: true),
                    CreateddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDegree", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EducationDegree_StudentEducation_EducationId",
                        column: x => x.EducationId,
                        principalTable: "StudentEducation",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAddressCollection",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    StudentSeekersStudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAddressCollection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentAddressCollection_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAddressCollection_StudentEducation_StudentID",
                        column: x => x.StudentID,
                        principalTable: "StudentEducation",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAddressCollection_Students_StudentSeekersStudentID",
                        column: x => x.StudentSeekersStudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAddressCollection_AddressID",
                table: "CompanyAddressCollection",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAddressCollection_CompanyID",
                table: "CompanyAddressCollection",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyID",
                table: "CompanyContacts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDegree_EducationId",
                table: "EducationDegree",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_CompanyID",
                table: "JobModel",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddressCollection_AddressID",
                table: "StudentAddressCollection",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddressCollection_StudentID",
                table: "StudentAddressCollection",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAddressCollection_StudentSeekersStudentID",
                table: "StudentAddressCollection",
                column: "StudentSeekersStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEducation_StudentID",
                table: "StudentEducation",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExperience_StudentID",
                table: "StudentExperience",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyAddressCollection");

            migrationBuilder.DropTable(
                name: "CompanyContacts");

            migrationBuilder.DropTable(
                name: "EducationDegree");

            migrationBuilder.DropTable(
                name: "JobModel");

            migrationBuilder.DropTable(
                name: "StudentAddressCollection");

            migrationBuilder.DropTable(
                name: "StudentExperience");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "StudentEducation");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
