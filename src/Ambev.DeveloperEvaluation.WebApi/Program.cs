using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
	public static void Main(string[] args)
	{
		try
		{
			Log.Information("Starting web application");

			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
			builder.AddDefaultLogging();

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			builder.AddBasicHealthChecks();

			builder.Services.AddSwaggerGen(c =>
			{
				var jwtSecurityScheme = new OpenApiSecurityScheme
				{
					BearerFormat = "JWT",
					Name = "JWT Authentication",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = JwtBearerDefaults.AuthenticationScheme,
					Description = "Put only your JWT Bearer token on textbox below!",

					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};

				c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{ jwtSecurityScheme, Array.Empty<string>() }
				});
			});

			builder.Services.AddDbContext<DefaultContext>(options =>
			{
				options.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
				);
				//options.ConfigureWarnings(warnings => { warnings.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning); });
			});

			builder.Services.AddJwtAuthentication(builder.Configuration);

			//EventBus
			builder.Services.AddSingleton<InMemoryMessageQueue>();
			builder.Services.AddSingleton<IEventBus, EventBus>();

			builder.RegisterDependencies();

			builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssemblies(
					typeof(ApplicationLayer).Assembly,
					typeof(Program).Assembly
				);
			});

			builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var _Db = scope.ServiceProvider.GetRequiredService<DefaultContext>();

				if (_Db != null)
				{
					if (_Db.Database.GetPendingMigrations().Any())
					{
						_Db.Database.Migrate();
					}
				}
			}

			app.UseMiddleware<ValidationExceptionMiddleware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseBasicHealthChecks();

			app.MapControllers();

			app.Run();
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Application terminated unexpectedly");
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}
}
