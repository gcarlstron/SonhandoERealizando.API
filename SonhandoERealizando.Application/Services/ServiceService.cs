using SonhandoERealizando.Application.Services.Interfaces;
using SonhandoERealizando.Domain.Entities;
using SonhandoERealizando.Domain.Interfaces;

namespace SonhandoERealizando.Application.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    public async Task<List<ServiceDto>> GetAllAsync()
    {
        var services = await serviceRepository.GetAllAsync();
        var serviceDtoList = services.Select(EntityToDto).ToList();
        return serviceDtoList;
    }

    public async Task<ServiceDto?> GetByIdAsync(string id)
    {
        var service = await serviceRepository.GetByIdAsync(id);
        return service == null ? null : EntityToDto(service);
    }

    public async Task<Service> CreateAsync(ServiceDto serviceDto)
    {
        if (string.IsNullOrWhiteSpace(serviceDto.Name) || string.IsNullOrWhiteSpace(serviceDto.Description))
            throw new ArgumentException("Nome e descrição são obrigatórios.");

        var service = new Service
        {
            Id = Guid.NewGuid().ToString(),
            Name = serviceDto.Name,
            Description = serviceDto.Description,
            Value = serviceDto.Value,
            Photos = serviceDto.Photos.Select(p => new Photos(
                p.Id ?? Guid.NewGuid().ToString(),
                p.Url, 
                p.Description
            )).ToList(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await serviceRepository.CreateAsync(service);
    }

    public async Task UpdateAsync(string id, ServiceDto serviceDto)
    {
        if (string.IsNullOrWhiteSpace(serviceDto.Name) || string.IsNullOrWhiteSpace(serviceDto.Description))
            throw new ArgumentException("Nome e descrição são obrigatórios.");

        var existingService = await serviceRepository.GetByIdAsync(id);
        if (existingService == null)
            throw new KeyNotFoundException("Serviço não encontrado.");

        var service = new Service
        {
            Id = id,
            Name = serviceDto.Name,
            Description = serviceDto.Description,
            Value = serviceDto.Value,
            Photos = serviceDto.Photos.Select(p => new Photos(
                p.Id ?? Guid.NewGuid().ToString(),
                p.Url, 
                p.Description
            )).ToList(),
            CreatedAt = existingService.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        await serviceRepository.UpdateAsync(id, service);
    }

    public async Task RemoveAsync(string id)
    {
        await serviceRepository.RemoveAsync(id);
    }

    private ServiceDto EntityToDto(Service service)
    {
        return new ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Value = service.Value,
            Photos = service.Photos.Select(p => new PhotoDto
            {
                Id = p.Id,
                Url = p.Url,
                Description = p.Description
            }).ToList(),
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt
        };
    }
}
