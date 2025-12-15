using Postgrest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingTracker.Models;

namespace TrainingApp.API.Models
{
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Login { get; set; }

		public string PasswordHash { get; set; }

		public string Username { get; set; }

		public DateTime CreatedAt { get; set; }

		public List<Training> Trainings { get; set; } = new List<Training>();
	}
}
