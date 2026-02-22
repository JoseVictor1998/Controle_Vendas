using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunicacaoVisual.API.DBModels;
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
                // Executa a Procedure de validação no SQL Server
                var usuario = _context.Database
                    .SqlQuery<UsuarioLoginDTO>($"EXEC SP_Validar_Login @Login={request.Login}, @Senha={request.Senha}")
                    .AsEnumerable()
                    .FirstOrDefault();

                if (usuario == null)
                    return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });

                // Gera o token baseado nos dados retornados pelo banco
                var tokenString = GerarJwtToken(usuario);

                return Ok(new
                {
                    id = usuario.UsuarioId,
                    nome = usuario.Nome,
                    nivel = usuario.NivelAcesso,
                    token = tokenString,
                    mensagem = "Logado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao processar login.", detalhe = ex.Message });
            }
        }

        private string GerarJwtToken(UsuarioLoginDTO usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Busca a chave secreta e garante que ela exista
            var jwtKey = _config["Jwt:Key"] ?? throw new Exception("Configuração Jwt:Key não encontrada.");
            var chave = Encoding.UTF8.GetBytes(jwtKey);

            // Monta as permissões (Claims)
            // Usamos nomes curtos para bater com o NameClaimType e RoleClaimType do Program.cs
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login ?? ""),
                new Claim(ClaimTypes.Role, usuario.NivelAcesso ?? "Vendedor"),
                new Claim("UsuarioId", usuario.UsuarioId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(10), // Token válido por 10 horas
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                )
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