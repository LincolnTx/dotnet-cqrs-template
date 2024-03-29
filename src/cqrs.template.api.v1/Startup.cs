using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cqrs.template.api.v1.Filters;
using cqrs.template.application.CommandHandlers;
using cqrs.template.infra.CrossCutting.IoC.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace cqrs.template.api.v1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApiVersioning();
			services.AddVersionedApiExplorer();
			services.AddSwaggerSetup();
			services.AddAutoMapper();
			services.AddDependencyInjectionSetup();
			services.AddMediatR(typeof(CommandHandler));
			services.AddScoped<GlobalExceptionFilterAttribute>();
			services.AddDatabaseSetup();
			
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder =>
			{
				builder.WithOrigins("*");
				builder.AllowAnyOrigin();
				builder.AllowAnyMethod();
				builder.AllowAnyHeader();
			});
			

			app.UseRouting();

			app.UseAuthorization();
			app.UseSwaggerSetup(provider);

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}