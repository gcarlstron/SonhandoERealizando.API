using SonhandoERealizando.Domain.Entities;

namespace SonhandoERealizando.Application.Services.Interfaces;

public interface IClientService
{
    Task<List<ClientDto>> GetAllAsync();
    Task<ClientDto?> GetByIdAsync(string id);
    Task<Client> CreateAsync(ClientDto cliente);
    Task UpdateAsync(string id, ClientDto clienteAtualizado);
    Task RemoveAsync(string id);
}