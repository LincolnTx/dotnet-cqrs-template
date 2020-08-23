using System;
using MediatR;

namespace cqrs.template.domain.Events
{
	public class Event : INotification
	{
		public DateTime Timestamp { get; set; }

		protected Event()
		{
			Timestamp = DateTime.Now;
		}
	}
}