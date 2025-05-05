using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SonhandoERealizando.Domain.Entities;
using SonhandoERealizando.Domain.Interfaces;

namespace SonhandoERealizando.Infrastructure.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly IMongoCollection<Lead> _leads;

    public LeadRepository(IConfiguration config)
    {
        try
        {
            var connectionString = config.GetConnectionString("MongoDB");
            Console.WriteLine($"Conectando ao MongoDB: {connectionString}");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("SonhandoERealizandoDB");
            _leads = database.GetCollection<Lead>("Leads");

            var indexKeys = Builders<Lead>.IndexKeys.Ascending(c => c.Id);
            var indexModel = new CreateIndexModel<Lead>(indexKeys);
            _leads.Indexes.CreateOne(indexModel);

            Console.WriteLine("Conexão com MongoDB estabelecida com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar ao MongoDB: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Lead>> GetAllAsync()
    {
        try
        {
            return await _leads.Find(lead => true).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar leads: {ex.Message}");
            throw;
        }
    }

    public async Task<Lead?> GetByIdAsync(string id) =>
        await _leads.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<Lead> CreateAsync(Lead lead)
    {
        await _leads.InsertOneAsync(lead);
        return lead;
    }

    public async Task UpdateAsync(string id, Lead leadAtualizado) =>
        await _leads.ReplaceOneAsync(c => c.Id == id, leadAtualizado);

    public async Task RemoveAsync(string id) =>
        await _leads.DeleteOneAsync(c => c.Id == id);
}
