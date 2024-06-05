namespace EventSourcingDemo.Shared.Events.Handlers
{
	public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
	{
		void Handle(TEvent command);
	}
	
}
