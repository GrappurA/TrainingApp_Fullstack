using Microsoft.EntityFrameworkCore;
using TrainingApp.API.Models;
using TrainingTracker.Models;

namespace TrainingTracker
{
	public class UserDbContext : DbContext
	{
		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

		public DbSet<User> Users => Set<User>();
	}
}
