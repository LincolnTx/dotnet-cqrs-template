using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace cqrs.template.infra.CrossCutting.IoC.Configurations
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
					Title = "Your Api Name",
					Description = "Some description",
					Contact = new OpenApiContact {Name = "YourName", Email = "youremail@email.com"}
					//you can add a contact URI with you want
					// Contact = new OpenApiContact { Name = "Lincoln", Email = "email@gmail.com", Url = new Uri("https://yoursite.com") }
				});

				// here it's adding a new authorization header to swagger ui, the default is JWT Bearer Token, but you can use anyone
				s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					In = ParameterLocation.Header,
					Scheme = "bearer",
					Description = "Jwt Token For authentication"
				});

				s.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new List<string>()
					}
				});

				s.OperationFilter<RemoveVersionParameterFilter>();
				s.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
				s.EnableAnnotations();
			});

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
		}

		public static void UseSwaggerSetup(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
		{
			if (app == null) throw new ArgumentNullException(nameof(app));

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				foreach (var description in provider.ApiVersionDescriptions)
				{
					c.SwaggerEndpoint($"{description.GroupName}/swagger.json",
						description.GroupName.ToUpperInvariant());
				}
			});
		}
		
		public class RemoveVersionParameterFilter : IOperationFilter
		{
			public void Apply(OpenApiOperation operation, OperationFilterContext context)
			{
				var versionParameter = operation.Parameters.Single(p => p.Name == "version");
				operation.Parameters.Remove(versionParameter);
			}
		}
		public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
		{
			public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
			{
				var paths = new OpenApiPaths();

				foreach (var path in swaggerDoc.Paths)
				{
					paths.Add(path.Key.Replace("{version}", swaggerDoc.Info.Version), path.Value);
				}

				swaggerDoc.Paths = paths;
			}
		}

		public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
		{
			private readonly IApiVersionDescriptionProvider _provider;

			public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
				_provider = provider;
			public void Configure(SwaggerGenOptions options)
			{
				foreach (var description in _provider.ApiVersionDescriptions)
				{
					options.SwaggerDoc(
						description.GroupName,
						new OpenApiInfo
						{
							Title = $"Add your api name here {description.ApiVersion}",
							Version = description.ApiVersion.ToString()
						});
				}
			}
		}
	}
}