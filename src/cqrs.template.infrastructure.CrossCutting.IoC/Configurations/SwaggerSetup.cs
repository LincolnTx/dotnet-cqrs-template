using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace cqrs.template.infrastructure.CrossCutting.IoC.Configurations
{
	public static class SwaggerSetup
	{
		public static void AddSwaggerSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Your Api Name",
					Description = "Some description",
					Contact = new OpenApiContact { Name = "Lincoln", Email = "lincolnsf98@gmail.com", Url = new Uri("") }
				});
			});
		}
		
		public static void UseSwaggerSetup(this IApplicationBuilder app)
		{
			if (app == null) throw new ArgumentNullException(nameof(app));
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "A dotnet CQRS template made by Lincoln Teixeira");
			});
		}
	}
}