using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TrainingApp.API.DTOs;
using TrainingApp.API.Models;
using TrainingTracker;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TrainingApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UserController(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}

		public static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public static bool VerifyPassword(string password, string hash)
		{
			return BCrypt.Net.BCrypt.Verify(password, hash);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserDto userDto)
		{
			bool exists = await _context.Users.AsNoTracking().AnyAsync(u => u.Login == userDto.Login);
			if (!exists)
			{
				try
				{
					User newUser = new User
					{
						CreatedAt = DateTime.UtcNow,
						Login = userDto.Login,
						PasswordHash = HashPassword(userDto.Password),
						Username = userDto.Username
					};

					WriteColoredMessageToConsole($"NEW USER REGISTERED {newUser.Username} | {newUser.Login} !", ConsoleColor.Green);
					var token = GenerateGwtToken(userDto.Login);

					await _context.AddAsync(newUser);
					await _context.SaveChangesAsync();

					return Ok(new
					{
						userId = newUser.Id,
						token = token
					});

				}
				catch (Exception e)// error occured 
				{
					WriteColoredMessageToConsole("error adding user: " + e.Message, ConsoleColor.Red);

					if (e.Message.Length > 30)
					{
						return BadRequest("An error occured");
					}
					return BadRequest(e.Message);
				}
			}
			else //user exists
			{
				WriteColoredMessageToConsole("UserAlreadyExsists!", ConsoleColor.DarkRed);
				return BadRequest("User already exsists!");
			}
		}

		private void WriteColoredMessageToConsole(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.White;
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login([FromBody] UserDto userDto)
		{
			if (userDto == null)
				return BadRequest("Invalid User");

			User existingUser = await _context.Users
				.AsNoTracking()
				.FirstAsync(u => u.Login == userDto.Login);


			if (VerifyPassword(userDto.Password, existingUser.PasswordHash))
			{
				WriteColoredMessageToConsole($"USER {existingUser.Username} | {existingUser.Login} LOGGED IN!", ConsoleColor.Green);
				var token = GenerateGwtToken(userDto.Login);
				return Ok(new
				{
					token = token
				});
			}
			else
			{
				WriteColoredMessageToConsole("USER DID NOT LOG IN!", ConsoleColor.Red);
				return BadRequest("Log in operation failed: wrong email or password!");
			}

		}

		[HttpGet("getusers")]
		public async Task<IActionResult> GetUsers()
		{
			var _users = await _context.Users.AsNoTracking().ToListAsync();
			return Ok(_users);
		}

		[HttpDelete("deleteall")]
		public async Task<IActionResult> DeleteAllUsers()
		{
			await _context.Users.ExecuteDeleteAsync();
			await _context.SaveChangesAsync();
			return Ok();
		}

		private string GenerateGwtToken(string login)
		{
			var claims = new[]
			{
				new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, login),
				new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TrainingTracker_env")));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: "localhost:5173",
				audience: "localhost:5173",
				claims: claims,
				expires: DateTime.UtcNow.AddDays(7),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
