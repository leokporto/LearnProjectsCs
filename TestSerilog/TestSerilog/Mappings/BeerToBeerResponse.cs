using TestSerilog.DTOs;

namespace TestSerilog.Mappings;

public static class BeerToBeerResponse
{
    public static GetBeersResponse ToGetBeersResponse(this Entity.Beer beer)
    {
        return new GetBeersResponse
        {
            Id = beer.Id,
            Name = beer.Name,
            Description = beer.Description,
            Abv = beer.Abv,
            Ibu = beer.Ibu
        };
    }
}