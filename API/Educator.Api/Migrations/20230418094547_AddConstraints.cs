using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educator.Api.Migrations
{
	/// <inheritdoc />
	public partial class AddConstraints : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(
								@"ALTER TABLE [dbo].[Enrollments]
								ADD CONSTRAINT FK_SubjectDetailsEnrollments
								FOREIGN KEY ([SubjectDetailId]) REFERENCES [dbo].[SubjectDetails]([Id]);

								ALTER TABLE [dbo].[Enrollments]
								ADD CONSTRAINT FK_StudentsEnrollments
								FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Users]([Id]);

								ALTER TABLE [dbo].[Replacements]
								ADD CONSTRAINT FK_EnrollmentsReplacements
								FOREIGN KEY ([EnrollmentId]) REFERENCES [dbo].[Enrollments]([Id]);

								ALTER TABLE [dbo].[Replacements]
								ADD CONSTRAINT FK_OldStudentReplacements
								FOREIGN KEY ([OldStudentId]) REFERENCES [dbo].[Users]([Id]);

								ALTER TABLE [dbo].[Replacements]
								ADD CONSTRAINT FK_NewStudentReplacements
								FOREIGN KEY ([NewStudentId]) REFERENCES [dbo].[Users]([Id]);

								ALTER TABLE [dbo].[Replacements]
								ADD CONSTRAINT FK_StatusesReplacements
								FOREIGN KEY ([StatusId]) REFERENCES [dbo].[ReplacementStatuses]([Id]);

								ALTER TABLE [dbo].[SubjectDetails]
								ADD CONSTRAINT FK_SubjectsSubjectDetails
								FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects]([Id]);

								ALTER TABLE [dbo].[SubjectDetails]
								ADD CONSTRAINT FK_TeachersSubjectDetails
								FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Users]([Id]);

								ALTER TABLE [dbo].[SubjectDetails]
								ADD CONSTRAINT FK_BuildingsSubjectDetails
								FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings]([Id]);

								ALTER TABLE [dbo].[Users]
								ADD CONSTRAINT FK_RoleUsers
								FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id]);

								ALTER TABLE [dbo].[Users] ALTER COLUMN [Login] NVARCHAR (50) NOT NULL;

								ALTER TABLE [dbo].[Users]
								ADD CONSTRAINT UC_User UNIQUE ([Id],[Login]);

								ALTER TABLE [dbo].[Buildings] ALTER COLUMN [Name] NVARCHAR (150) NOT NULL;
								ALTER TABLE [dbo].[Buildings]
								ADD CONSTRAINT UC_Building UNIQUE ([Id],[Name]);

								ALTER TABLE [dbo].[Subjects] ALTER COLUMN [Name] NVARCHAR (150) NOT NULL;
								ALTER TABLE [dbo].[Subjects]
								ADD CONSTRAINT UC_Subject UNIQUE ([Id],[Name]);

								ALTER TABLE [dbo].[ReplacementStatuses] ALTER COLUMN [Name] NVARCHAR (50) NOT NULL;
								ALTER TABLE [dbo].[ReplacementStatuses]
								ADD CONSTRAINT UC_Status UNIQUE ([Id],[Name]);

								ALTER TABLE [dbo].[Roles] ALTER COLUMN [Name] NVARCHAR (50) NOT NULL;
								ALTER TABLE [dbo].[Roles]
								ADD CONSTRAINT UC_Role UNIQUE ([Id],[Name]);");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"ALTER TABLE [dbo].[Enrollments]
								DROP CONSTRAINT FK_SubjectDetailsEnrollments;

								ALTER TABLE [dbo].[Enrollments]
								DROP CONSTRAINT FK_StudentsEnrollments;

								ALTER TABLE [dbo].[Replacements]
								DROP CONSTRAINT FK_EnrollmentsReplacements;

								ALTER TABLE [dbo].[Replacements]
								DROP CONSTRAINT FK_OldStudentReplacements;

								ALTER TABLE [dbo].[Replacements]
								DROP CONSTRAINT FK_NewStudentReplacements;

								ALTER TABLE [dbo].[Replacements]
								DROP CONSTRAINT FK_StatusesReplacements;

								ALTER TABLE [dbo].[SubjectDetails]
								DROP CONSTRAINT FK_SubjectsSubjectDetails;

								ALTER TABLE [dbo].[SubjectDetails]
								DROP CONSTRAINT FK_TeachersSubjectDetails;

								ALTER TABLE [dbo].[SubjectDetails]
								DROP CONSTRAINT FK_BuildingsSubjectDetails;

								ALTER TABLE [dbo].[Users]
								DROP CONSTRAINT FK_RoleUsers;

								ALTER TABLE [dbo].[Users]
								DROP CONSTRAINT UC_User;

								ALTER TABLE [dbo].[Buildings]
								DROP CONSTRAINT UC_Building;

								ALTER TABLE [dbo].[Subjects]
								DROP CONSTRAINT UC_Subject;

								ALTER TABLE [dbo].[ReplacementStatuses]
								DROP CONSTRAINT UC_Status;

								ALTER TABLE [dbo].[Roles]
								DROP CONSTRAINT UC_Role;"
			);
		}
	}
}
