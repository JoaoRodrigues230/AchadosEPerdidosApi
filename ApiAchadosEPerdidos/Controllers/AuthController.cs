using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAchadosEPerdidos.Models;
using BCrypt.Net; 

namespace ApiAchadosEPerdidos.Controllers
{
    [Route("api/autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AchadosContext _context;

        public AuthController(AchadosContext context)
        {
            _context = context;
        }

        [HttpPost("criar-usuario")]
        public async Task<ActionResult> CriarUsua(Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
                return BadRequest("Email já cadastrado.");

            if (!string.IsNullOrEmpty(usuario.Cpf) && await _context.Usuarios.AnyAsync(u => u.Cpf == usuario.Cpf))
                return BadRequest("CPF já cadastrado.");

            if (!string.IsNullOrEmpty(usuario.Ra) && await _context.Usuarios.AnyAsync(u => u.Ra == usuario.Ra))
                return BadRequest("RA já cadastrado.");
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            if (usuario.Id == Guid.Empty) usuario.Id = Guid.NewGuid();

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            usuario.Senha = "";
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Ra == request.Ra);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            return Ok(new { 
                mensagem = "Logado com sucesso!",
                usuario = new { usuario.Id, usuario.Nome, usuario.Email, usuario.Tipo }
            });
        }
    }

    public class LoginRequest
    {
        public string Ra { get; set; }
        public string Senha { get; set; }
    }
}