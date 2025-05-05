using SonhandoERealizando.Application.Services.Interfaces;
using SonhandoERealizando.Domain.Entities;
using SonhandoERealizando.Domain.Interfaces;

namespace SonhandoERealizando.Application.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<List<ClientDto>> GetAllAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        var clientDtoList = clients.Select(EntityToDto).ToList();
        return clientDtoList;
    }

    public async Task<ClientDto?> GetByIdAsync(string id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        return client == null ? null : EntityToDto(client);
    }

    public async Task<Client> CreateAsync(ClientDto cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Name) || string.IsNullOrWhiteSpace(cliente.Email))
            throw new ArgumentException("Nome e email são obrigatórios.");

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            Name = cliente.Name,
            Email = cliente.Email,
            Phone = cliente.Phone,
            Quotes = cliente.Quotes.Select(q => new Quote
            {
                Id = q.Id,
                ServiceId = q.ServiceId,
                Description = q.Description,
                Value = q.Value,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt
            }).ToList(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await clientRepository.CreateAsync(client);
    }

    public async Task UpdateAsync(string id, ClientDto clienteAtualizado)
    {
        if (string.IsNullOrWhiteSpace(clienteAtualizado.Name) || string.IsNullOrWhiteSpace(clienteAtualizado.Email))
            throw new ArgumentException("Nome e email são obrigatórios.");

        var existingClient = await clientRepository.GetByIdAsync(id);
        if (existingClient == null)
            throw new KeyNotFoundException("Cliente não encontrado.");

        var client = new Client
        {
            Id = id,
            Name = clienteAtualizado.Name,
            Email = clienteAtualizado.Email,
            Phone = clienteAtualizado.Phone,
            Quotes = clienteAtualizado.Quotes.Select(q => new Quote
            {
                Id = q.Id,
                ServiceId = q.ServiceId,
                Description = q.Description,
                Value = q.Value,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt
            }).ToList(),
            CreatedAt = existingClient.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        await clientRepository.UpdateAsync(id, client);
    }

    public async Task RemoveAsync(string id)
    {
        await clientRepository.RemoveAsync(id);
    }

    private ClientDto EntityToDto(Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email,
            Phone = client.Phone,
            Quotes = client.Quotes.Select(q => new QuoteDto
            {
                Id = q.Id,
                ServiceId = q.ServiceId,
                Description = q.Description,
                Value = q.Value,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt
            }).ToList(),
            CreatedAt = client.CreatedAt,
            UpdatedAt = client.UpdatedAt
        };
    }
}
