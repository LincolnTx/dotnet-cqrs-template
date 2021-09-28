using System.Collections.Generic;
 using cqrs.template.api.v1.Filters;
 using cqrs.template.domain.Exceptions;
 using MediatR;
 using Microsoft.AspNetCore.Mvc;

namespace cqrs.template.api.v1.Controllers
{
	[Route("your-project-name/[controller]/v{version:apiVersion}")]
	[ServiceFilter(typeof(GlobalExceptionFilterAttribute))]
	public class BaseController : Controller
	{
		private readonly ExceptionNotificationHandler _notifications;

		protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

		protected BaseController(INotificationHandler<ExceptionNotification> notifications)
		{
			_notifications = (ExceptionNotificationHandler) notifications;
		}

		protected bool IsValidOperation()
		{
			return (!_notifications.HasNotifications());
		}

		protected new IActionResult Response(IActionResult action)
		{
			if (IsValidOperation())
			{
				return action;
			}

			return BadRequest
			(
				new
				{
					success = false,
					errors = _notifications.GetNotifications()
				}
			);
		}
	}
}