using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SonhandoERealizando.Domain.Entities;

public class Client
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public List<Quote> Quotes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}