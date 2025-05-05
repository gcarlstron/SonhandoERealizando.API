using Microsoft.AspNetCore.Mvc;
using SonhandoERealizando.Application.Services;
using SonhandoERealizando.Application.Services.Interfaces;

namespace SonhandoERealizando.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadController(ILeadService leadService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await leadService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LeadDto leadDto)
    {
        var lead = await leadService.CreateAsync(leadDto);
        return CreatedAtAction(nameof(Get), new { lead.Id }, lead);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var lead = await leadService.GetByIdAsync(id);
        if (lead == null) return NotFound();
        return Ok(lead);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] LeadDto lead)
    {
        await leadService.UpdateAsync(id, lead);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await leadService.RemoveAsync(id);
        return NoContent();
    }
}