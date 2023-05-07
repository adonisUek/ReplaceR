using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educator.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusColumnToReplacements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Replacements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Replacements");
        }
    }
}
