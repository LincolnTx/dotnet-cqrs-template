using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace cqrs.template.domain.Exceptions
{
	public class ExceptionNotificationHandler : INotificationHandler<ExceptionNotification>
	{
		private List<ExceptionNotification> _notifications;
		
		public ExceptionNotificationHandler()
		{
			_notifications = new List<ExceptionNotification>();
		}
		
		public Task Handle(ExceptionNotification message, CancellationToken cancellationToken)
		{
			_notifications.Add(message);
			
			return Task.CompletedTask;
		}

		public virtual List<ExceptionNotification> GetNotifications()
		{
			return _notifications;
		}

		public virtual bool HasNotifications()
		{
			return GetNotifications().Any();
		}

		public void Dispose()
		{
			_notifications = new List<ExceptionNotification>();
		}
	}
}