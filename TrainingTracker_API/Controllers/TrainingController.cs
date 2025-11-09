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
		public async Task<IActionResult> PostTraining([FromBody] Training training)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (_context.Training.Count() == 0)
			{
				training.TrainingId = 1;
			}
			else
			{
				training.TrainingId = _context.Training.Count() + 1;
			}

			await _context.Entry(training).ReloadAsync();

			await _context.AddAsync(training);
			await _context.SaveChangesAsync();
			return Ok(training);
		}

		[HttpDelete("deletetraining/{trainingId}")]
		public async Task<IActionResult> DeleteTraining(int trainingId)
		{
			var trainingToDelete = await _context.Training.FirstOrDefaultAsync(tr=>tr.TrainingId == trainingId);
			try
			{
				if (trainingToDelete == null)
				{
					throw new Exception("Training was null");
				}
				_context.Remove(trainingToDelete);
				_context.SaveChanges();
				return Ok(trainingToDelete);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception druing deleting the training: " + ex.Message);
				return BadRequest();
				throw;
			}
		}

	}
}
