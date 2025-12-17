using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixContactCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Contacts",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Contacts",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contacts",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Contacts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Contacts",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Contacts",
                newName: "Id");
        }
    }
}
