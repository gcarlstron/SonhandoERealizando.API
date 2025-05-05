using SonhandoERealizando.Application.Services;
using SonhandoERealizando.Domain.Entities;

namespace SonhandoERealizando.Application.Services.Interfaces;

public interface ILeadService
{
    Task<List<LeadDto>> GetAllAsync();
    Task<LeadDto?> GetByIdAsync(string id);
    Task<Lead> CreateAsync(LeadDto lead);
    Task UpdateAsync(string id, LeadDto lead);
    Task RemoveAsync(string id);
}
