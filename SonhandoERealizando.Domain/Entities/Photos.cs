namespace SonhandoERealizando.Domain.Entities;

public class Photos
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Photos(string id, string url, string description)
    {
        Id = id;
        Url = url;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
