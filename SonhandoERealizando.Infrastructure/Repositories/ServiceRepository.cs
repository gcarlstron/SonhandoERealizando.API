using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SonhandoERealizando.Domain.Entities;
using SonhandoERealizando.Domain.Interfaces;

namespace SonhandoERealizando.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly IMongoCollection<Service> _services;

    public ServiceRepository(IConfiguration config)
    {
        try
        {
            var connectionString = config.GetConnectionString("MongoDB");
            Console.WriteLine($"Conectando ao MongoDB: {connectionString}");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("SonhandoERealizandoDB");
            _services = database.GetCollection<Service>("Services");

            var indexKeys = Builders<Service>.IndexKeys.Ascending(c => c.Id);
            var indexModel = new CreateIndexModel<Service>(indexKeys);
            _services.Indexes.CreateOne(indexModel);

            Console.WriteLine("Conexão com MongoDB estabelecida com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar ao MongoDB: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Service>> GetAllAsync()
    {
        try
        {
            return await _services.Find(service => true).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar serviços: {ex.Message}");
            throw;
        }
    }

    public async Task<Service?> GetByIdAsync(string id) =>
        await _services.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<Service> CreateAsync(Service service)
    {
        await _services.InsertOneAsync(service);
        return service;
    }

    public async Task UpdateAsync(string id, Service serviceAtualizado) =>
        await _services.ReplaceOneAsync(c => c.Id == id, serviceAtualizado);

    public async Task RemoveAsync(string id) =>
        await _services.DeleteOneAsync(c => c.Id == id);
}