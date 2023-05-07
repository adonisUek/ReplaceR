using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educator.Api.Migrations
{
    /// <inheritdoc />
    public partial class LoginUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE [dbo].[Users]
                                ADD CONSTRAINT UC_Login UNIQUE ([Login]);
                                ALTER TABLE [dbo].[Subjects]
                                ADD CONSTRAINT UC_SubjectName UNIQUE ([Name]);
                                ALTER TABLE [dbo].[Buildings]
                                ADD CONSTRAINT UC_BuildingName UNIQUE ([Name]);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE [dbo].[Users]
                                DROP CONSTRAINT UC_Login;
                                ALTER TABLE [dbo].[Subjects]
                                DROP CONSTRAINT UC_SubjectName;
                                ALTER TABLE [dbo].[Buildings]
                                DROP CONSTRAINT UC_BuildingName;");
        }
    }
}
