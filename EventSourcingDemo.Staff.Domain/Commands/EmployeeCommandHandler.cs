using EventSourcingDemo.Shared.Commands.Handlers;
using EventSourcingDemo.Shared.ExternalServices;
using EventSourcingDemo.Staff.Domain.Events;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class EmployeeCommandHandler :
		ICommandHandler<AddEmployeeCommand>
	{
		private IEventStoreService _eventStoreService;

		public EmployeeCommandHandler(IEventStoreService eventStoreService) 
		{
			_eventStoreService = eventStoreService;
		}


		public async Task HandleAsync(AddEmployeeCommand command)
		{
			var addEvent = new EmployeeAddedEvent() 
			{
				EmployeeId = command.EmployeeId,
				FullName = command.FullName,
				DateOfBirth = command.DateOfBirth,
				IsActive = true
			};
			await _eventStoreService.AppendAsync(addEvent);
		}
	}
}
