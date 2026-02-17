using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunicacaoVisual.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ControleVendasContext _context;
        private readonly IConfiguration _config;

        public AuthController(ControleVendasContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Usamos SqlQuery para mapear apenas as colunas que a procedure retorna
                var usuario = (await _context.Database
                    .SqlQuery<UsuarioLoginDTO>($"EXEC SP_Validar_Login @Login={request.Login}, @Senha={request.Senha}")
                    .ToListAsync())
                    .FirstOrDefault();

                if (usuario != null)
                {
                    return Ok(new
                    {
                        id = usuario.UsuarioId,
                        nome = usuario.Nome,
                        nivel = usuario.NivelAcesso, // Aqui você identifica o "Login Deus"
                        mensagem = "Logado com sucesso!"
                    });
                }

                return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });
            }
            catch (Exception ex)
            {
                // Captura o RAISERROR da sua procedure
                return StatusCode(401, new { erro = ex.Message });
            }
        }

        private string GerarJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes("HIqZPFh1CXELeez3lXTi");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            // O ?? "" remove o aviso de erro de nulo
            new Claim(ClaimTypes.Name, usuario.Login ?? ""),
            new Claim(ClaimTypes.Role, usuario.NivelAcesso ?? "Vendedor"),
            new Claim("UsuarioId", usuario.UsuarioId.ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public required string Login { get; set; }
        public required string Senha { get; set; }
    }
}