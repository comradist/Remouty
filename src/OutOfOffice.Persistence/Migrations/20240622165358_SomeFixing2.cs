using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SomeFixing2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "PeoplePartnerId",
                table: "Employees",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "PeoplePartnerId",
                table: "Employees",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
