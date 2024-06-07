namespace EventSourcingDemo.Shared.Queries
{
	public interface IQueryHandler<in TQuery> 
		where TQuery : IQueryRequest 
	{
		IQueryResponse Handle(TQuery queryRequest);
	}
}
