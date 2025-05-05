using SonhandoERealizando.Domain.Entities;

namespace SonhandoERealizando.Domain.Interfaces;

public interface ILeadRepository
{
    Task<List<Lead>> GetAllAsync();
    Task<Lead?> GetByIdAsync(string id);
    Task<Lead> CreateAsync(Lead lead);
    Task UpdateAsync(string id, Lead lead);
    Task RemoveAsync(string id);
}
