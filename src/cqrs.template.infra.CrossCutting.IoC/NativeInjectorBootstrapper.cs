using MediatR;
using System;
using FluentValidation;
using cqrs.template.domain.Exceptions;
using cqrs.template.application.Behaviour;
using Microsoft.Extensions.DependencyInjection;

namespace cqrs.template.infra.CrossCutting.IoC
{
	public static class NativeInjectorBootstrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			RegisterData(services);
			RegisterMediatR(services);
		}

		private static void RegisterData(IServiceCollection services)
		{
			// here goes your repository injection
			// sample: services.AddScoped<IUserRepository, UserRepository>();
		}

		private static void RegisterMediatR(IServiceCollection services)
		{
			const string applicationAssemblyName = "cqrs.template.application"; // use your project name
			var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);
			
			AssemblyScanner
				.FindValidatorsInAssembly(assembly)
				.ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
			
			// injection for Mediator
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));
			services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
		}
	}
}