using Microsoft.AspNetCore.Mvc;
using Hypesoft.Domain.Entities;
using System.Collections.Generic;
using System.Linq; // necessário para Any()

namespace Hypesoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // Lista de usuários em memória
    private static List<User> _users = new()
    {
        // Usuários iniciais para testes
        new User { Login = "admin", Password = "1234", Role = "Admin" },
        new User { Login = "user", Password = "1234", Role = "User" }
    };

    // ✅ Método público para o AuthController acessar os usuários
    public static List<User> GetUsers() => _users;

    // POST /api/users -> cadastrar usuário
    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
            return BadRequest("Login e senha são obrigatórios");

        // Checa se login já existe
        if (_users.Any(u => u.Login == user.Login))
            return BadRequest("Login já existe");

        _users.Add(user);
        return Ok(user);
    }

    // GET /api/users -> listar todos os usuários
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_users);
    }
}