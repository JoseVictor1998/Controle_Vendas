using ComunicacaoVisual.API.Contracts;
using ComunicacaoVisual.API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly ControleVendasContext _context;

        public PessoasController(ControleVendasContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "God,Admin")]
        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioInput model)
        {
            try
            {
                // 🚀 O PULO DO GATO: Trocamos a Procedure que não existe por um INSERT direto na tabela!
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    INSERT INTO Usuario (Nome, Funcao, Login, Senha, Nivel_Acesso)
                    VALUES ({model.Nome}, {model.Nivel_Acesso}, {model.Login}, {model.Senha}, {model.Nivel_Acesso})
                ");

                return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao cadastrar usuário", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpPost("CadastrarCliente")]

        public async Task<IActionResult> CadastrarCliente([FromBody] CadastrarClienteCompletoInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Cadastrar_Cliente_Completo 
                        @Nome = {model.Nome}, 
                        @Email = {model.Email}, 
                        @DDD = {model.DDD}, 
                        @NumeroTelefone = {model.NumeroTelefone}, 
                        @Cidade = {model.Cidade}, 
                        @CEP = {model.CEP}, 
                        @Bairro = {model.Bairro}, 
                        @Rua = {model.Rua}, 
                        @NumeroEndereco = {model.NumeroEndereco}, 
                        @Documento = {model.Documento}, 
                        @Tipo = {model.Tipo}");
                return Ok(new { mensagem = "Cliente cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao cadastrar cliente", detalhe = ex.Message });
            }

        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("ClientesListar")]
        public async Task<IActionResult> ClientesListar()
        {
            var lista = await _context.Clientes
                .Select(c => new { id = c.ClienteId, nome = c.Nome })
                .OrderBy(x => x.nome)
                .ToListAsync();

            return Ok(lista);
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("PesquisaClienteVendas")]
        public async Task<IActionResult> GetPesquisaClienteVenda([FromQuery] string? filtro)
        {
            try
            {
                var consulta = _context.VwPesquisaClientesVendas.AsQueryable();
                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta = consulta.Where(p =>
                     p.Nome.Contains(filtro) ||
                    (p.Documento != null && p.Documento.Contains(filtro)));
                }
                var resultado = await consulta.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro em Pesquisa Cliente Venda",
                    erro = ex.Message
                });
            }

        }


    }
}