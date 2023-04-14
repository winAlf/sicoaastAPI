using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sicoaastAPI.Migrations
{
    /// <inheritdoc />
    public partial class campoNombreCompletoEnMovEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "MovEmp",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "MovEmp");
        }
    }
}
