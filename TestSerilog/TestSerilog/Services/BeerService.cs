using TestSerilog.Contracts;
using TestSerilog.DTOs;
using TestSerilog.Mappings;

namespace TestSerilog.Services;

public class BeerService : IBeerService
{
    private readonly IBeerRepository _beerRepository;

    public BeerService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }
    
    
    public IEnumerable<GetBeersResponse> GetAllBeers()
    {
        var beers = _beerRepository.List();
        IList<GetBeersResponse> beerResponses = new List<GetBeersResponse>();
        
        if (beers == null || !beers.Any())
        {
            return Enumerable.Empty<GetBeersResponse>();
        }

        foreach (var beer in beers)
        {
            beerResponses.Add(beer.ToGetBeersResponse());
        }
        
        return beerResponses ;
    }
}



