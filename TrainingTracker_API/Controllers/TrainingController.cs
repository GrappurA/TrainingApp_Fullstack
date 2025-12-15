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
			var _trainings = await _context.Trainings.AsNoTracking().ToListAsync();
			return Ok(_trainings);
		}

		[HttpPost("posttraining")]//in work, fix the id bullshit
		public async Task<IActionResult> PostTraining([FromBody] Training training)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//training.UserId =	

			await _context.AddAsync(training);
			await _context.SaveChangesAsync();
			return Ok(training);
		}

		[HttpDelete("deletetraining/{trainingId}")]
		public async Task<IActionResult> DeleteTraining(int trainingId)
		{
			try
			{
				var rowsAffectedDelete = await _context.Trainings.Where(tr => tr.TrainingId == trainingId).ExecuteDeleteAsync();
				if (rowsAffectedDelete == 0)
				{
					return Ok("raining already deleted or did not exist.");
				}
				return Ok("Deleted successfully");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception during deleting training: " + ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch("patchtraining/{id}")]
		public async Task<IActionResult> PutTraining(int trainingId, [FromBody] Training newTraining)
		{
			try
			{
				var rowsAffectedUpdate = await _context.Trainings.Where(tr => tr.TrainingId == trainingId).ExecuteUpdateAsync(upd => upd
				.SetProperty(t => t.Name, newTraining.Name)
				.SetProperty(t => t.DateTime, newTraining.DateTime)
				.SetProperty(t => t.Calories, newTraining.Calories)
				.SetProperty(t => t.Duration, newTraining.Duration)
				.SetProperty(t => t.Description, newTraining.Description)
				);

				if (rowsAffectedUpdate == 0)
					return NotFound("No rows was found");
				return Ok("Training was updated!");
			}
			catch (Exception ex)
			{
				return BadRequest("Error while updating training: " + ex.Message);
			}
		}
		

	}
}
