using Microsoft.EntityFrameworkCore;
using TrainingTracker.Models;

namespace TrainingTracker
{
	public class TrainingDbContext : DbContext
	{
		public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options) { }

		public DbSet<Training> Training => Set<Training>();
	}
}
