using EventSourcingDemo.Shared.Commands;
using EventSourcingDemo.Shared.Commands.Handlers;
using EventSourcingDemo.Shared.Enums;
using EventSourcingDemo.Shared.Events;
using EventSourcingDemo.Shared.ExternalServices;
using EventSourcingDemo.Staff.Domain.Enums;
using EventSourcingDemo.Staff.Domain.Events;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class EmployeeCommandHandler :
		ICommandHandler<AddEmployeeCommand>,
		ICommandHandler<UpdateEmployeeCommand>,
		ICommandHandler<UpdateEmployeeRoleCommand>,
		ICommandHandler<DismissEmployeeCommand>
	{
		private IEventStoreService _eventStoreService;

		public EmployeeCommandHandler(IEventStoreService eventStoreService) 
		{
			_eventStoreService = eventStoreService;
		}


		public async Task<ICommandResponse> HandleAsync(AddEmployeeCommand command)
		{
			var addEvent = new EmployeeAddedEvent() 
			{
				EmployeeId = Guid.NewGuid(),
				FullName = command.FullName,
				DateOfBirth = command.DateOfBirth,
				IsActive = true
			};

			try
			{
				await _eventStoreService.AppendAsync(addEvent);
				return new EmployeeCommandResponse { Status = eResponseStatus.Success };
			}
			catch (Exception ex)
			{
				return new EmployeeCommandResponse { Status = eResponseStatus.Failure, Message = ex.Message };
			}
			
		}

		public async Task<ICommandResponse> HandleAsync(UpdateEmployeeCommand command)
		{
			var updateEvent = new EmployeeUpdatedEvent(command.EmployeeId)
			{				
				FullName = command.FullName,
				Email = command.Email
			};

			try
			{
				await _eventStoreService.AppendAsync(updateEvent);
				return new EmployeeCommandResponse { Status = eResponseStatus.Success };
			}
			catch (Exception ex)
			{
				return new EmployeeCommandResponse { Status = eResponseStatus.Failure, Message = ex.Message };
			}
		}

		public async Task<ICommandResponse> HandleAsync(UpdateEmployeeRoleCommand command)
		{
			IDomainEvent assignEvent;
			try
			{
				switch (command.Operation)
				{
					case eRoleOperation.Assign:
						assignEvent = new EmployeeRoleAssignedEvent(command.EmployeeId, command.RoleName);
						break;
					case eRoleOperation.Remove:
						assignEvent = new EmployeeRoleRemovedEvent(command.EmployeeId, command.RoleName);
						break;
					default:
						return new EmployeeCommandResponse { Status = eResponseStatus.Failure, Message = "Invalid operation" };
				}

				await _eventStoreService.AppendAsync(assignEvent);
				return new EmployeeCommandResponse { Status = eResponseStatus.Success };
			}
			catch (Exception ex)
			{
				return new EmployeeCommandResponse { Status = eResponseStatus.Failure, Message = ex.Message };
			}
		}

		public async Task<ICommandResponse> HandleAsync(DismissEmployeeCommand command)
		{
			var dismissEvent = new EmployeeDismissedEvent(command.EmployeeId);
			try 
			{ 
				await _eventStoreService.AppendAsync(dismissEvent);
				return new EmployeeCommandResponse { Status = eResponseStatus.Success };
			}
			catch(Exception ex)
			{
				return new EmployeeCommandResponse { Status = eResponseStatus.Failure, Message = ex.Message };
			}
		}
	}
}
