namespace EventSourcingDemo.Shared.Commands.Handlers
{
	public interface ICommandHandler<in TCommand> where TCommand : ICommandRequest
	{
		Task HandleAsync(TCommand command);
	}
}
