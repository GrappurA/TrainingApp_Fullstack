using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TrainingTracker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			string connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

			// Add services to the container.

			builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseNpgsql(connectionString));  /*,op =>
			op.EnableRetryOnFailure(
				maxRetryCount: 5,
				maxRetryDelay: TimeSpan.FromSeconds(20),
				errorCodesToAdd: null)));
			*/
			builder.Services.AddDbContext<AppDbContext>(options =>
			options.UseNpgsql(connectionString)); /*,op =>
			op.EnableRetryOnFailure(
				maxRetryCount: 5,
				maxRetryDelay: TimeSpan.FromSeconds(20),
				errorCodesToAdd: null)));
			//, ServerVersion.AutoDetect(connectionString)));
			*/

			//adding jwt
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				var env = Environment.GetEnvironmentVariable("TrainingTracker_env");

				options.TokenValidationParameters = new TokenValidationParameters
				{

					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = "localhost:5173",
					ValidAudience = "localhost:5173",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TrainingTracker_env")))
				};
			});
			builder.Services.AddAuthorization();


			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", p =>
					p.AllowAnyOrigin()
					 .AllowAnyMethod()
					 .AllowAnyHeader());
			});

			var app = builder.Build();

			app.UseCors("AllowAll");


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}