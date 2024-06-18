using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Out_of_Office.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Subdivision = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeoplePartnerID = table.Column<int>(type: "int", nullable: false),
                    OutOfOfficeBalance = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    AbsenceReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeaveRequest_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectManagerID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Project_Employee_ProjectManagerID",
                        column: x => x.ProjectManagerID,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalRequest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApproverID = table.Column<int>(type: "int", nullable: false),
                    LeaveRequestID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApprovalRequest_Employee_ApproverID",
                        column: x => x.ApproverID,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalRequest_LeaveRequest_LeaveRequestID",
                        column: x => x.LeaveRequestID,
                        principalTable: "LeaveRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequest_ApproverID",
                table: "ApprovalRequest",
                column: "ApproverID");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequest_LeaveRequestID",
                table: "ApprovalRequest",
                column: "LeaveRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_EmployeeID",
                table: "LeaveRequest",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectManagerID",
                table: "Project",
                column: "ProjectManagerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalRequest");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "LeaveRequest");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
