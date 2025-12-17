using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

			//adding jwt service 
			/*
			builder.Services.AddScoped<TrainingApp.API.Services.JwtService>();
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer
				.JwtBearerDefaults.AuthenticationScheme;

				options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer
				.JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
					System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
				};
			});
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

			//app.UseAuthorization();
			app.UseAuthentication();

			app.MapControllers();

			app.Run();
		}
	}
}