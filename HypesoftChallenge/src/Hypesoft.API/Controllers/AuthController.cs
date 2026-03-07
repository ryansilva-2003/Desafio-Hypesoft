using Microsoft.AspNetCore.Mvc;
using Hypesoft.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hypesoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        if (loginUser == null || string.IsNullOrEmpty(loginUser.Login) || string.IsNullOrEmpty(loginUser.Password))
            return BadRequest("Login ou senha não informados");

        // Pega os usuários via método público
        var users = UsersController.GetUsers();

        var user = users.FirstOrDefault(u =>
            u.Login == loginUser.Login && u.Password == loginUser.Password);

        if (user == null)
            return Unauthorized("Login ou senha inválidos");

        // Checa se a chave JWT está configurada
        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new Exception("A chave JWT não está configurada no appsettings.json");

        var key = Encoding.ASCII.GetBytes(jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return Ok(new { token = jwt });
    }
}