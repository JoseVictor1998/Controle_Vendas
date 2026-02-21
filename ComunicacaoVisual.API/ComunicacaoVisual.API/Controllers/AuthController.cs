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
                // No AuthController.cs
                var usuario = _context.Database
                    .SqlQuery<UsuarioLoginDTO>($"EXEC SP_Validar_Login @Login={request.Login}, @Senha={request.Senha}")
                    .AsEnumerable() // Traz os dados para a memória antes de filtrar
                    .FirstOrDefault();

                if (usuario == null)
                    return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });

                // Criando objeto para gerar token
                var userParaToken = new Usuario
                {
                    Login = request.Login,
                    NivelAcesso = usuario.NivelAcesso,
                    UsuarioId = usuario.UsuarioId
                };

                var tokenString = GerarJwtToken(userParaToken);

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
                // Retorna mensagem de erro genérica
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        private string GerarJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtKey = _config["Jwt:Key"];
            Console.WriteLine("JWT KEY (AuthController): " + jwtKey);

            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new Exception("Jwt:Key NÃO carregou no AuthController.");

            var chave = Encoding.UTF8.GetBytes(jwtKey);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, usuario.Login ?? ""),
        new Claim(ClaimTypes.Role, usuario.NivelAcesso ?? "Vendedor"),
        new Claim("UsuarioId", usuario.UsuarioId.ToString())
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    // DTO para login recebido do cliente
    public class LoginRequest
    {
        public required string Login { get; set; }
        public required string Senha { get; set; }
    }
}
