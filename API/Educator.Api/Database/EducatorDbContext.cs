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
		public DbSet<Activity> Activities { get; set; }
		public DbSet<ActivityStatus> ActivityStatuses { get; set; }
		public DbSet<User> Users { get; set; }
	}
}