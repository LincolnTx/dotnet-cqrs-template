using System;
using System.Collections.Generic;
using MediatR;

namespace cqrs.template.domain.SeedWork
{
	public class Entity
	{
		private int? _requestedHashCode;
		private string _Id;

		public virtual string Id
		{
			get { return _Id; }
		}

		protected void SetId()
		{
			_Id = Guid.NewGuid().ToString();
		} 
		
			

		#region Entity
		public bool IsTransient()
		{
			return this.Id == default(string);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Entity))
				return false;

			if (Object.ReferenceEquals(this, obj))
				return true;

			if (this.GetType() != obj.GetType())
				return false;

			Entity item = (Entity) obj;

			if (item.IsTransient() || this.IsTransient())
				return false;
			else
				return item.Id == this.Id;
		}

		public override int GetHashCode()
		{
			if (!IsTransient())
			{
				if (!_requestedHashCode.HasValue)
					_requestedHashCode = this.Id.GetHashCode() ^ 31;
				
				return _requestedHashCode.Value;
			}
			else 
				return base.GetHashCode();
		}

		public static bool operator ==(Entity left, Entity right)
		{
			if (Object.Equals(left, null))
				return (Object.Equals(right, null)) ? true : false;
			else
				return left.Equals(right);
		}

		public static bool operator !=(Entity left, Entity right)
		{
			return !(left == right);
		}
		#endregion

		#region domain events

		private List<INotification> _domainEvents;
		public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

		public void AddDomainEvent(INotification eventItem)
		{
			_domainEvents = _domainEvents ?? new List<INotification>();
			_domainEvents.Add(eventItem);
		}

		public void RemoveDomainEvent(INotification eventItem)
		{
			_domainEvents?.Remove(eventItem);
		}

		public void ClearDomainEvent()
		{
			_domainEvents?.Clear();
		}
		#endregion
	}
}