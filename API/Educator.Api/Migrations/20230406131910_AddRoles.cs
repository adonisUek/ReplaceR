using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educator.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var schema = "dbo";
			migrationBuilder.Sql($"SET IDENTITY_INSERT [dbo].[Roles] ON;" +
                $"INSERT INTO [{schema}].[Roles] ([Id], [Name]) VALUES (1, 'Admin'), (2, 'Teacher'), (3, 'Student');" +
                $"SET IDENTITY_INSERT [dbo].[Roles] OFF;");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var schema = "dbo";
			migrationBuilder.Sql($"TRUNCATE TABLE [{schema}].[Roles];");
		}
    }
}
