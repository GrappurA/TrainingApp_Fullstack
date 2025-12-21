using Microsoft.EntityFrameworkCore;
using TrainingTracker.Models;
using TrainingApp.API.Models;

namespace TrainingTracker
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Training> Trainings => Set<Training>();

		public DbSet<User> Users => Set<User>();
	}
}
