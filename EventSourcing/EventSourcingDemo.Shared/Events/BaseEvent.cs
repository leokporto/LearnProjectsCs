namespace EventSourcingDemo.Shared.Events
{
	public abstract class BaseEvent : IDomainEvent 
	{
		public BaseEvent()
		{
			CreatedAtUtc = DateTime.UtcNow;
		}

		public abstract Guid StreamId { get; }

		public DateTime CreatedAtUtc { get; }

	}

}
