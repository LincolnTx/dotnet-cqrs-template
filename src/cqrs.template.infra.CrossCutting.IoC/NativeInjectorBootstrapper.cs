using System;
using AutoMapper.Configuration;
using cqrs.template.domain.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace cqrs.template.infra.CrossCutting.IoC
{
	public class NativeInjectorBootstrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			RegisterData(services);
			RegisterMediatR(services);
		}

		public static void RegisterData(IServiceCollection services)
		{
			// here goes your repository injection
			// sample: services.AddScoped<IUserRepository, UserRepository>();
		}

		public static void RegisterMediatR(IServiceCollection services)
		{
			// injection for Mediator
			services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
		}
	}
}