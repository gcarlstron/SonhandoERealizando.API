using SonhandoERealizando.Application.Services;
using SonhandoERealizando.Domain.Entities;

namespace SonhandoERealizando.Application.Services.Interfaces;

public interface IServiceService
{
    Task<List<ServiceDto>> GetAllAsync();
    Task<ServiceDto?> GetByIdAsync(string id);
    Task<Service> CreateAsync(ServiceDto service);
    Task UpdateAsync(string id, ServiceDto service);
    Task RemoveAsync(string id);
}
