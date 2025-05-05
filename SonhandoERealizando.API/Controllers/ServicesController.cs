using Microsoft.AspNetCore.Mvc;
using SonhandoERealizando.Application.Services;
using SonhandoERealizando.Application.Services.Interfaces;

namespace SonhandoERealizando.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(IServiceService serviceService) : ControllerBase
{
     [HttpGet]
     public async Task<IActionResult> Get() => Ok(await serviceService.GetAllAsync());

     [HttpPost]
     public async Task<IActionResult> Post([FromBody] ServiceDto serviceDto)
     {
         var service = await serviceService.CreateAsync(serviceDto);
         return CreatedAtAction(nameof(Get), new { service.Id }, service);
     }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var service = await serviceService.GetByIdAsync(id);
        if (service == null) return NotFound();
        return Ok(service);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] ServiceDto service)
    {
        await serviceService.UpdateAsync(id, service);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await serviceService.RemoveAsync(id);
        return NoContent();
    }
}