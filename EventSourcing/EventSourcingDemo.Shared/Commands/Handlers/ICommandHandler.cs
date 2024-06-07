namespace EventSourcingDemo.Shared.Commands.Handlers
{
	public interface ICommandHandler<in TCommand> where TCommand : ICommandRequest
	{
		Task<ICommandResponse> HandleAsync(TCommand command);
	}
}
