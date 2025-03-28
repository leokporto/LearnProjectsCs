using TestSerilog.DTOs;
using TestSerilog.Entity;

namespace TestSerilog.Contracts;

public interface IBeerService
{
    public IEnumerable<GetBeersResponse> GetAllBeers();
}