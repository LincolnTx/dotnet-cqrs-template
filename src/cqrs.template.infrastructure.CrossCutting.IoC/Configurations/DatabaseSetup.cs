using System;
using cqrs.template.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace cqrs.template.infrastructure.CrossCutting.IoC.Configurations
{
	public static class DatabaseSetup
	{
		public static void AddDatabaseSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
		}
	}
}