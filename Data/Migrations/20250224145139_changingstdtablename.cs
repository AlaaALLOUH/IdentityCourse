using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityCourse.Data.Migrations
{
    /// <inheritdoc />
    public partial class changingstdtablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "StudentsModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsModel",
                table: "StudentsModel",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsModel",
                table: "StudentsModel");

            migrationBuilder.RenameTable(
                name: "StudentsModel",
                newName: "Students");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");
        }
    }
}
