using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SonhandoERealizando.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        // Simulação de validação de usuário e senha
        var validUser = loginDto.Username == "admin" && VerifyPassword(loginDto.Password, "admin");

        if (!validUser)
            return Unauthorized("Usuário ou senha inválidos.");

        return Ok("Login realizado com sucesso.");
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        using var sha256 = SHA256.Create();
        var hashedInput = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return hashedInput == hashedPassword;
    }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
