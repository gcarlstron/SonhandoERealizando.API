namespace SonhandoERealizando.Application.Services;

public class ServiceDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PhotoDto> Photos { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
