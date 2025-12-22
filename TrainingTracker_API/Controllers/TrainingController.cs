using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingTracker.Models;
using TrainingApp.API.Models;
using System.Security.Claims;
using TrainingApp.API.DTOs;

namespace TrainingTracker.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class TrainingController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TrainingController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("gettraining")] //of a specific user
		public async Task<ActionResult<IEnumerable<Training>>> GetTraining()
		{
			string userLogin = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid userId = await _context
				.Users.AsNoTracking()
				.Where(u => u.Login == userLogin)
				.Select(u => u.Id)
				.FirstOrDefaultAsync();

			var _trainings = await _context
				.Trainings
				.AsNoTracking()
				.Where(t => t.UserId == userId)
				.Select(t => t)
				.ToListAsync();
			return Ok(_trainings);
		}

		[HttpPost("posttraining")]
		public async Task<IActionResult> PostTraining([FromBody] Training training)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//validating info for the specific user
			string userLogin = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid userId = await _context.Users.AsNoTracking().Where(u => u.Login == userLogin).Select(u => u.Id).FirstOrDefaultAsync();

			training.UserId = userId;

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
		public async Task<IActionResult> PutTraining([FromBody] TrainingDto newTraining)
		{
			try
			{
				var rowsAffectedUpdate = await _context.Trainings.Where(tr => tr.Id == newTraining.Id).ExecuteUpdateAsync(upd => upd
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
