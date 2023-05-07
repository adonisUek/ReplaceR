using Educator.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Educator.Api.Database
{
	public class EducatorDbContext : DbContext
	{
		public EducatorDbContext(DbContextOptions<EducatorDbContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			try
			{
				string connectionString = File.ReadAllText("bin/Config/EducatorConnectionString.txt");
				optionsBuilder.UseSqlServer(connectionString);
			}
			catch (Exception)
			{
				throw;
			}
		}
		public DbSet<Building> Buildings { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Replacement> Replacements { get; set; }
		public DbSet<ReplacementStatus> ReplacementStatuses { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<SubjectDetail> SubjectDetails { get; set; }
		public DbSet<User> Users { get; set; }
	}
}