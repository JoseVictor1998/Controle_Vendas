using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunicacaoVisual.API.Models;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ControleVendasContext _context;

        public AuthController(ControleVendasContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Login == request.Login && u.Senha == request.Senha);

                if (usuario != null)
                {
                    // Retornamos um objeto limpo para o Front-end
                    return Ok(new
                    {
                        id = usuario.UsuarioId,
                        nome = usuario.Nome,
                        nivelAcesso = usuario.NivelAcesso, // Ex: Admin, Vendedor, Producao
                        mensagem = "Logado com sucesso!"
                    });
                }

                return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro no servidor", detalhe = ex.Message });
            }
        }
    }

}