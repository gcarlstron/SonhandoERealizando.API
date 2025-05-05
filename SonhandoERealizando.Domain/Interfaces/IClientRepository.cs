using SonhandoERealizando.Domain.Entities;

namespace SonhandoERealizando.Domain.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(string id);
    Task<Client> CreateAsync(Client cliente);
    Task UpdateAsync(string id, Client clienteAtualizado);
    Task RemoveAsync(string id);
}