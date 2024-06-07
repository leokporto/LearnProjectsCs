using EventSourcingDemo.Shared.ExternalServices;
using EventSourcingDemo.Shared.Queries;
using EventSourcingDemo.Staff.Domain.Entities;

namespace EventSourcingDemo.Staff.Domain.Queries
{
	public class EmployeeQueriesHandler :
		IQueryHandler<GetEmployeeQuery>,
		IQueryHandler<GetEmployeeEventsQuery>
	{
		private IEventStoreService _eventStore;
		public EmployeeQueriesHandler(IEventStoreService eventStoreService)
		{
			_eventStore = eventStoreService;
		}

		

		public IQueryResponse Handle(GetEmployeeQuery queryRequest)
		{
			EmployeeQueryResponse response = null;
			if (queryRequest == null || queryRequest.EmployeeId == Guid.Empty)
			{
				return new EmployeeQueryResponse() { Status = Shared.Enums.eResponseStatus.Failure, Message = "EmployeeId is required" };
			}

			try
			{
				var result = _eventStore.Find<Employee>(queryRequest.EmployeeId);
				response = new EmployeeQueryResponse() 
				{ 
					Status = Shared.Enums.eResponseStatus.Success, 
					Data = result 
				};
			}
			catch (Exception ex)
			{
				return new EmployeeQueryResponse() { Status = Shared.Enums.eResponseStatus.Failure, Message = ex.Message };
			}

			return response;
		}

		public  IQueryResponse Handle(GetEmployeeEventsQuery queryRequest)
		{
			EmployeeQueryResponse response = null;
			if (queryRequest == null || queryRequest.EmployeeId == Guid.Empty)
			{
				return new EmployeeQueryResponse() { Status = Shared.Enums.eResponseStatus.Failure, Message = "EmployeeId is required" };
			}

			try
			{
				var result = _eventStore.ListStreamEvents(queryRequest.EmployeeId);
				response = new EmployeeQueryResponse() 
				{ 
					Status = Shared.Enums.eResponseStatus.Success, 
					Data = result 
				};
			}
			catch (Exception ex)
			{
				return new EmployeeQueryResponse() { Status = Shared.Enums.eResponseStatus.Failure, Message = ex.Message };
			}

			return response;

		}
	}
}
