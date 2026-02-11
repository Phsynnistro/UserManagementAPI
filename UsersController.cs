using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models; // Certifique-se de que o nome do projeto bate com o seu
using System.Collections.Generic;
using System.Linq;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Lista estática para simular um banco de dados em memória
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Admin User", Email = "admin@techhive.com" },
            new User { Id = 2, Name = "Test User", Email = "test@techhive.com" }
        };

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        // GET: api/users/{id}
        // Correção: Agora trata o erro se o ID não existir
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = $"Usuário com ID {id} não encontrado." });
            }

            return Ok(user);
        }

        // POST: api/users
        // Correção: Adicionado Try-Catch e validação automática via [ApiController]
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            try
            {
                if (newUser == null)
                {
                    return BadRequest("Dados do usuário inválidos.");
                }

                // Lógica simples para gerar o próximo ID (evita duplicidade manual)
                newUser.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;

                users.Add(newUser);

                // Retorna 201 Created e o local onde o novo recurso pode ser encontrado
                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
            }
            catch (System.Exception ex)
            {
                // Log do erro (simulado)
                // Console.WriteLine(ex.Message);
                
                // Retorna erro 500 em vez de travar a API
                return StatusCode(500, new { Message = "Ocorreu um erro interno ao criar o usuário.", Details = ex.Message });
            }
        }
    }
}
