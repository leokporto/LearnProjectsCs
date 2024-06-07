using EventSourcingDemo.Shared.Enums;

namespace EventSourcingDemo.Shared.Contracts
{
    public interface IStatusResponse
    {
        eResponseStatus Status { get; }

        string? Message { get; }
    }
}
