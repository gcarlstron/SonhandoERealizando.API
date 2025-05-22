using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SonhandoERealizando.Application.Services;
using SonhandoERealizando.Application.Services.Interfaces;


namespace SonhandoERealizando.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientesController(IClientService clientService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await clientService.GetAllAsync());

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] ClientDto clientDto)
    {
        var client = await clientService.CreateAsync(clientDto);
        return CreatedAtAction(nameof(Get), new { client.Id }, client);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var client = await clientService.GetByIdAsync(id);
        if (client == null) return NotFound();
        return Ok(client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] ClientDto client)
    {
        await clientService.UpdateAsync(id, client);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await clientService.RemoveAsync(id);
        return NoContent();
    }
}
