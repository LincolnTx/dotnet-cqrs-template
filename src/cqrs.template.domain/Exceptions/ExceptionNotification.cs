using cqrs.template.domain.Events;

namespace cqrs.template.domain.Exceptions
{
	public class ExceptionNotification : Event
	{
		public string Code { get; private set; }
		public string Message { get; private set; }
		public string ParamName { get; private set; }

		public ExceptionNotification(string code, string message, string paramName = null)
		{
			Code = code;
			Message = message;
			ParamName = paramName;
		}
	}
}