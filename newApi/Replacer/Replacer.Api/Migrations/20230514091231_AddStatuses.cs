using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Replacer.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var schema = "dbo";
			migrationBuilder.Sql($"SET IDENTITY_INSERT [{schema}].[ActivityStatuses] ON;" +
				$"INSERT INTO [{schema}].[ActivityStatuses] ([Id], [Name]) VALUES " +
				$"(1, 'Available')," +
				$"(2, 'Reserved')," +
				$"(3, 'Cancelled');" +
				$"SET IDENTITY_INSERT [{schema}].[ActivityStatuses] OFF;");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var schema = "dbo";
			migrationBuilder.Sql($"DELETE FROM [{schema}].[ActivityStatuses] WHERE [Id] BETWEEN 1 AND 3;");
		}
    }
}
