namespace SonhandoERealizando.Application.Services;

public class ClientDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<QuoteDto> Quotes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

