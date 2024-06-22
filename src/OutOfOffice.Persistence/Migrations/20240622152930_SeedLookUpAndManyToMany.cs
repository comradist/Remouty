using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutOfOffice.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedLookUpAndManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_RequestStatuses_StatusID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Subdivisions_SubdivisionID",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Subdivisions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RequestStatuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProjectTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProjectStatuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Positions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SubdivisionID",
                table: "Employees",
                newName: "SubdivisionId");

            migrationBuilder.RenameColumn(
                name: "StatusID",
                table: "Employees",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "PositionID",
                table: "Employees",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SubdivisionID",
                table: "Employees",
                newName: "IX_Employees_SubdivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_StatusID",
                table: "Employees",
                newName: "IX_Employees_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PositionID",
                table: "Employees",
                newName: "IX_Employees_PositionId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AbsenceReasons",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectEmployee",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployee", x => new { x.ProjectId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_ProjectEmployee_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEmployee_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AbsenceReasons",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Vacation" },
                    { 2, "Sick Leave" },
                    { 3, "Business Trip" },
                    { 4, "Maternity Leave" },
                    { 5, "Paternity Leave" },
                    { 6, "Unpaid Leave" },
                    { 7, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Junior Developer" },
                    { 2, "Middle Developer" },
                    { 3, "Senior Developer" },
                    { 4, "Team Lead" },
                    { 5, "HR Manager" },
                    { 6, "Project Manager" },
                    { 7, "Business Analyst" },
                    { 8, "QA Engineer" }
                });

            migrationBuilder.InsertData(
                table: "ProjectStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "In Progress" },
                    { 2, "Completed" },
                    { 3, "On Hold" },
                    { 4, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "ProjectTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Internal" },
                    { 2, "External" },
                    { 3, "R&D" },
                    { 4, "Maintenance" },
                    { 5, "Development" }
                });

            migrationBuilder.InsertData(
                table: "RequestStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cancelled" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "New" }
                });

            migrationBuilder.InsertData(
                table: "Subdivisions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Development" },
                    { 2, "HR" },
                    { 3, "QA" },
                    { 4, "Management" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployee_EmployeeId",
                table: "ProjectEmployee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_RequestStatuses_StatusId",
                table: "Employees",
                column: "StatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Subdivisions_SubdivisionId",
                table: "Employees",
                column: "SubdivisionId",
                principalTable: "Subdivisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Positions_PositionId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_RequestStatuses_StatusId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Subdivisions_SubdivisionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "ProjectEmployee");

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AbsenceReasons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProjectStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subdivisions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subdivisions",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RequestStatuses",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProjectTypes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProjectStatuses",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Positions",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SubdivisionId",
                table: "Employees",
                newName: "SubdivisionID");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Employees",
                newName: "StatusID");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Employees",
                newName: "PositionID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SubdivisionId",
                table: "Employees",
                newName: "IX_Employees_SubdivisionID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_StatusId",
                table: "Employees",
                newName: "IX_Employees_StatusID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                newName: "IX_Employees_PositionID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AbsenceReasons",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Positions_PositionID",
                table: "Employees",
                column: "PositionID",
                principalTable: "Positions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_RequestStatuses_StatusID",
                table: "Employees",
                column: "StatusID",
                principalTable: "RequestStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Subdivisions_SubdivisionID",
                table: "Employees",
                column: "SubdivisionID",
                principalTable: "Subdivisions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
