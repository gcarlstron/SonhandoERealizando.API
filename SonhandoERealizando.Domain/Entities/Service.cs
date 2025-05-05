namespace SonhandoERealizando.Domain.Entities;

public class Service
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Photos> Photos { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}