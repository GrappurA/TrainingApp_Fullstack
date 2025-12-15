using Microsoft.EntityFrameworkCore;
using TrainingTracker.Models;

namespace TrainingTracker
{
	public class TrainingDbContext : DbContext
	{
		public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options) { }

		public DbSet<Training> Trainings => Set<Training>();
	}
}
