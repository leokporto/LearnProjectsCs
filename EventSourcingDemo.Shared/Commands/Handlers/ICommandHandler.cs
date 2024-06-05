namespace EventSourcingDemo.Shared.Commands.Handlers
{
	public interface ICommandHandler<in TCommand> where TCommand : ICommand
	{
		Task HandleAsync(TCommand command);
	}
}
