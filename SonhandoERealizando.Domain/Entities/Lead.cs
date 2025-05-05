using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SonhandoERealizando.Domain.Entities;

public class Lead
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}