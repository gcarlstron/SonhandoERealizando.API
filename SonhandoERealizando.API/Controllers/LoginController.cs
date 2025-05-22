using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SonhandoERealizando.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginDto login)
    {
        var validUser = login.Username == "admin" && VerifyPassword(login.Password, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=");

        if (!validUser)
            return Unauthorized("Usuário ou senha inválidos.");

        var basicAuthHash = GenerateBasicAuthHash(login.Username, login.Password);

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
