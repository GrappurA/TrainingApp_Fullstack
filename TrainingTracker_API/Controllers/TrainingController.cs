using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingTracker.Models;

namespace TrainingTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TrainingController : Controller
	{
		private readonly TrainingDbContext _context;

		public TrainingController(TrainingDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Training>>> GetTraining()
		{
			var _trainings = await _context.Training.ToListAsync();
			return Ok(_trainings);
		}

		[HttpPost]
		public async Task PostTraining(Training training)
		{
			//validate smth

			await _context.AddAsync(training);
			await _context.SaveChangesAsync();
		}

	}
}
