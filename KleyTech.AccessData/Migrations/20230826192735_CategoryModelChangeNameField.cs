using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KleyTech.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryModelChangeNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Categories",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Nombre");
        }
    }
}
