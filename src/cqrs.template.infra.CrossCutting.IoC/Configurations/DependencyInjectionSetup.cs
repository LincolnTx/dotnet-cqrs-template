using System;
using Microsoft.Extensions.DependencyInjection;

namespace cqrs.template.infra.CrossCutting.IoC.Configurations
{
	public static class DependencyInjectionSetup
	{
		public static void AddDependencyInjectionSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			NativeInjectorBootstrapper.RegisterServices(services);
		}
	}
}