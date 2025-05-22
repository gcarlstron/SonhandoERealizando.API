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
        var validUser = loginDto.Username == "admin" && VerifyPassword(loginDto.Password, "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918");

        if (!validUser)
            return Unauthorized("Usuário ou senha inválidos.");

        // Gerar o hash de autenticação básica
        var basicAuthHash = GenerateBasicAuthHash(loginDto.Username, loginDto.Password);

        return Ok(new { Message = "Login realizado com sucesso.", BasicAuthHash = basicAuthHash });
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        using var sha256 = SHA256.Create();
        var hashedInput = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return hashedInput == hashedPassword;
    }

    private string GenerateBasicAuthHash(string username, string password)
    {
        var credentials = $"{username}:{password}";
        var bytes = Encoding.UTF8.GetBytes(credentials);
        return Convert.ToBase64String(bytes);
    }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
