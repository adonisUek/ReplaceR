using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educator.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddReplacementStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var schema = "dbo";
			migrationBuilder.Sql($"SET IDENTITY_INSERT [{schema}].[ReplacementStatuses] ON;" +
                $"INSERT INTO [{schema}].[ReplacementStatuses] ([Id], [Name]) VALUES " +
                $"(1, 'Available')," +
                $"(2, 'Reserved')," +
                $"(3, 'Cancelled');" +
                $"SET IDENTITY_INSERT [{schema}].[ReplacementStatuses] OFF;");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var schema = "dbo";
			migrationBuilder.Sql($"DELETE FROM [{schema}].[ReplacementStatuses] WHERE [Id] BETWEEN 1 AND 3;");
		}
    }
}