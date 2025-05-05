namespace SonhandoERealizando.Application.Services;

public class QuoteDto
{
    public string Id { get; set; }
    public string ServiceId { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
