using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SonhandoERealizando.Domain.Entities;
using SonhandoERealizando.Domain.Interfaces;
using System;

namespace SonhandoERealizando.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly IMongoCollection<Client> _clientes;

    public ClientRepository(IConfiguration config)
    {
        try
        {
            var connectionString = config.GetConnectionString("MongoDB");
            Console.WriteLine($"Conectando ao MongoDB: {connectionString}");
            
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("SonhandoERealizandoDB");
            _clientes = database.GetCollection<Client>("Clientes");

            var indexKeys = Builders<Client>.IndexKeys.Ascending(c => c.Id);
            var indexModel = new CreateIndexModel<Client>(indexKeys);
            _clientes.Indexes.CreateOne(indexModel);
            
            Console.WriteLine("Conexão com MongoDB estabelecida com sucesso");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar ao MongoDB: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Client>> GetAllAsync()
    {
        try
        {
            return await _clientes.Find(cliente => true).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar clientes: {ex.Message}");
            throw;
        }
    }

    public async Task<Client?> GetByIdAsync(string id) =>
        await _clientes.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<Client> CreateAsync(Client cliente)
    {
        await _clientes.InsertOneAsync(cliente);
        return cliente;
    }

    public async Task UpdateAsync(string id, Client clienteAtualizado) =>
        await _clientes.ReplaceOneAsync(c => c.Id == id, clienteAtualizado);

    public async Task RemoveAsync(string id) =>
        await _clientes.DeleteOneAsync(c => c.Id == id);
}
