using System;
using AutoMapper;
using cqrs.template.application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace cqrs.template.infra.CrossCutting.IoC.Configurations
{
	public static class AutoMapperSetup
	{
		public static void AddAutoMapper(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddAutoMapper(typeof(MappingProfile));
		}
	}
}