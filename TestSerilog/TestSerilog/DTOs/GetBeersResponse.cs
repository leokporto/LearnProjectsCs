namespace TestSerilog.DTOs;

public class GetBeersResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Abv { get; set; }
    
    public string Ibu { get; set; }
}