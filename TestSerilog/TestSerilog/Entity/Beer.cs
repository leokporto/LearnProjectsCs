namespace TestSerilog.Entity;

public class Beer
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Abv { get; set; }
    
    public string Ibu { get; set; }
    
    public int? StyleId { get; set; }
    
    public int? GlassId { get; set; }
}