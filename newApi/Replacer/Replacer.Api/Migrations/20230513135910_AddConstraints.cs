using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.Metrics;


#nullable disable

namespace Replacer.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE [dbo].[Activities]
                                    ADD CONSTRAINT FK_NewUserId
                                    FOREIGN KEY ([NewUserId]) REFERENCES [dbo].[Users]([Id]);

                                    ALTER TABLE [dbo].[Activities]
                                    ADD CONSTRAINT FK_CreatorId
                                    FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[Users]([Id]);

                                    ALTER TABLE [dbo].[Activities]
                                    ADD CONSTRAINT FK_StatusId
                                    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[ActivityStatuses]([Id]);

                                    ALTER TABLE [dbo].[Users] ALTER COLUMN [Login] NVARCHAR (50) NOT NULL;

                                    ALTER TABLE [dbo].[Users]
                                    ADD CONSTRAINT UC_Login UNIQUE ([Login]);"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
			    ALTER TABLE[dbo].[Activities]
			    DROP CONSTRAINT FK_NewUserId;

			    ALTER TABLE[dbo].[Activities]
			    DROP CONSTRAINT FK_CreatorId;

			    ALTER TABLE[dbo].[Activities]
			    DROP CONSTRAINT FK_StatusId;

			    ALTER TABLE[dbo].[Users]
			    DROP CONSTRAINT UC_Login;
            ");

		}
    }
}
