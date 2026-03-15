using ComunicacaoVisual.API.Contracts;
using ComunicacaoVisual.API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly ControleVendasContext _context;

        public EstoqueController(ControleVendasContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "God,Admin,Producao")]
        [HttpGet("Materiais")]
        public async Task<IActionResult> GetMateriais()
        {
            try
            {
                var lista = await _context.Materials
                    .Select(m => new
                    {
                        MaterialId = m.MaterialId,
                        Nome = m.Nome,
                        Descricao = m.Descricao,
                        Ativo = m.Ativo
                    })
                    .OrderByDescending(m => m.Ativo).ThenBy(m => m.Nome)
                    .ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex) { return StatusCode(500, new { erro = ex.Message }); }
        }
        [Authorize(Roles = "God,Admin")]
        [HttpPost("CadastrarMaterial")]
        public async Task<IActionResult> CadastrarMaterial([FromBody] CadastrarMaterialInput model)
        {
            try
            {
                var novoMaterial = new Material
                {
                    Nome = model.Nome,
                    Descricao = string.IsNullOrWhiteSpace(model.Descricao) ? "" : model.Descricao,
                    Ativo = true
                };

                _context.Materials.Add(novoMaterial);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Material cadastrado com sucesso!" });
            }
            catch (Exception ex) { return StatusCode(500, new { erro = "Erro ao cadastrar", detalhe = ex.Message }); }
        }

        [Authorize(Roles = "God,Admin")]
        [HttpPut("AlternarStatusMaterial/{id:int}")]
        public async Task<IActionResult> AlternarStatusMaterial(int id)
        {
            try
            {
                var material = await _context.Materials.FindAsync(id);
                if (material == null) return NotFound("Material não encontrado.");

                material.Ativo = !material.Ativo;
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = material.Ativo ? "Material Ativado!" : "Material Desativado!" });
            }
            catch (Exception ex) { return StatusCode(500, new { erro = "Erro ao mudar status", detalhe = ex.Message }); }
        }

        [Authorize(Roles = "God,Admin")]
        [HttpPost("CadastrarTipoProduto")]
        public async Task<IActionResult> CadastrarTipoProduto([FromBody] CadastrarTipoProdutoInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Cadastrar_Tipo_Produto_Completo 
                        @Categoria_ID = {model.Categoria_ID}, 
                        @Nome = {model.Nome}, 
                        @Descricao_Tecnica = {model.Descricao_Tecnica}, 
                        @Usa_Adesivo = {model.Usa_Adesivo}, 
                        @Usa_Mascara = {model.Usa_Mascara}, 
                        @Material_ID_1 = {model.Material_ID_1}, 
                        @Material_ID_2 = {model.Material_ID_2}, 
                        @Material_ID_3 = {model.Material_ID_3}
                ");
                return Ok(new { mensagem = "Produto e materiais vinculados com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao cadastrar produto", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor,Producao")]
        [HttpGet("CategoriasListar")]
        public async Task<IActionResult> CategoriasListar()
        {
            try
            {
                var lista = await _context.CategoriaProdutos
                    .Where(c => c.Ativo == true)
                    .Select(c => new CategoriaOption { Id = c.CategoriaId, Nome = c.Nome })
                    .OrderBy(c => c.Nome)
                    .ToListAsync();
                return Ok(lista);
            }
            catch { return StatusCode(500, "Erro ao buscar categorias"); }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("TiposProdutoListar")]
        public async Task<IActionResult> TiposProdutoListar()
        {
            var lista = await _context.TipoProdutos
                .Select(t => new { id = t.TipoProdutoId, nome = t.Nome })
                .OrderBy(x => x.nome)
                .ToListAsync();

            return Ok(lista);
        }


    }
}