namespace EventSourcingDemo.Shared.Commands
{
	public interface ICommandResponse
	{
		bool Status { get; }

		string Message { get; }
	}
}
