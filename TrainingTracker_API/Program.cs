using Microsoft.EntityFrameworkCore;

namespace TrainingTracker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			string connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

			// Add services to the container.

			builder.Services.AddDbContext<TrainingDbContext>(options =>
			options.UseNpgsql(connectionString));  /*,op =>
			op.EnableRetryOnFailure(
				maxRetryCount: 5,
				maxRetryDelay: TimeSpan.FromSeconds(20),
				errorCodesToAdd: null)));
			*/
			builder.Services.AddDbContext<UserDbContext>(options =>
			options.UseNpgsql(connectionString)); /*,op =>
			op.EnableRetryOnFailure(
				maxRetryCount: 5,
				maxRetryDelay: TimeSpan.FromSeconds(20),
				errorCodesToAdd: null)));
			//, ServerVersion.AutoDetect(connectionString)));
			*/

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

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}