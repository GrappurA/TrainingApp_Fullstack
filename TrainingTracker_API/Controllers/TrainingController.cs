using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingTracker.Models;

namespace TrainingTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TrainingController : ControllerBase
	{
		private readonly TrainingDbContext _context;

		public TrainingController(TrainingDbContext context)
		{
			_context = context;
		}

		[HttpGet("gettraining")]
		public async Task<ActionResult<IEnumerable<Training>>> GetTraining()
		{
			var _trainings = await _context.Training.ToListAsync();
			return Ok(_trainings);
		}

		[HttpPost("posttraining")]
		public async Task<IActionResult> PostTraining([FromBody]Training training)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _context.Entry(training).ReloadAsync();

			await _context.AddAsync(training);
			await _context.SaveChangesAsync();
			return Ok(training);
		}

	}
}
