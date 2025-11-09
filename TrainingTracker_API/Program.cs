using Microsoft.EntityFrameworkCore;

namespace TrainingTracker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

			// Add services to the container.

			builder.Services.AddDbContext<TrainingDbContext>(options =>
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();

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
				//app.UseSwagger();
				//app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}