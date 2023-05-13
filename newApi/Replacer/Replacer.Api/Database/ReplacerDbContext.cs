using Replacer.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Replacer.Api.Database
{
	public class ReplacerDbContext : DbContext
	{
		public ReplacerDbContext(DbContextOptions<ReplacerDbContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			try
			{
				string connectionString = File.ReadAllText("bin/Config/ReplacerConnectionString.txt");
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