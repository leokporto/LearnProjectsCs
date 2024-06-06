namespace EventSourcingDemo.Shared.Events
{
	public interface IDomainEvent
	{
		Guid StreamId { get; }
		
		DateTime CreatedAtUtc { get; }
	}
}
