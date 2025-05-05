using SonhandoERealizando.Domain.Entities;

namespace SonhandoERealizando.Domain.Interfaces;

public interface IServiceRepository
{
    Task<List<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(string id);
    Task<Service> CreateAsync(Service service);
    Task UpdateAsync(string id, Service service);
    Task RemoveAsync(string id);
}
