using TestSerilog.Entity;

namespace TestSerilog.Contracts;

public interface IBeerRepository
{
    IEnumerable<Beer> List();
}