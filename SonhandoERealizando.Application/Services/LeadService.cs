using SonhandoERealizando.Application.Services.Interfaces;
using SonhandoERealizando.Domain.Entities;
using SonhandoERealizando.Domain.Interfaces;

namespace SonhandoERealizando.Application.Services;

public class LeadService(ILeadRepository leadRepository) : ILeadService
{
    public async Task<List<LeadDto>> GetAllAsync()
    {
        var leads = await leadRepository.GetAllAsync();
        var leadDtoList = leads.Select(EntityToDto).ToList();
        return leadDtoList;
    }

    public async Task<LeadDto?> GetByIdAsync(string id)
    {
        var lead = await leadRepository.GetByIdAsync(id);
        return lead == null ? null : EntityToDto(lead);
    }

    public async Task<Lead> CreateAsync(LeadDto leadDto)
    {
        if (string.IsNullOrWhiteSpace(leadDto.Name) || string.IsNullOrWhiteSpace(leadDto.Email))
            throw new ArgumentException("Nome e email são obrigatórios.");

        var lead = new Lead
        {
            Id = Guid.NewGuid().ToString(),
            Name = leadDto.Name,
            Email = leadDto.Email,
            Phone = leadDto.Phone
        };

        return await leadRepository.CreateAsync(lead);
    }

    public async Task UpdateAsync(string id, LeadDto leadDto)
    {
        if (string.IsNullOrWhiteSpace(leadDto.Name) || string.IsNullOrWhiteSpace(leadDto.Email))
            throw new ArgumentException("Nome e email são obrigatórios.");

        var existingLead = await leadRepository.GetByIdAsync(id);
        if (existingLead == null)
            throw new KeyNotFoundException("Lead não encontrado.");

        var lead = new Lead
        {
            Id = id,
            Name = leadDto.Name,
            Email = leadDto.Email,
            Phone = leadDto.Phone
        };

        await leadRepository.UpdateAsync(id, lead);
    }

    public async Task RemoveAsync(string id)
    {
        await leadRepository.RemoveAsync(id);
    }

    private LeadDto EntityToDto(Lead lead)
    {
        return new LeadDto
        {
            Id = lead.Id,
            Name = lead.Name,
            Email = lead.Email,
            Phone = lead.Phone
        };
    }
}
