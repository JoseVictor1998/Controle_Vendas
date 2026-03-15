using ComunicacaoVisual.API.Contracts;
using ComunicacaoVisual.API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtesController : ControllerBase
    {
        private readonly ControleVendasContext _context;
        private readonly IConfiguration _config;

        public ArtesController(ControleVendasContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [Authorize(Roles = "God,Admin,Arte,Vendedor")]
        [HttpGet("DownloadArte/{id:int}")]
        public async Task<IActionResult> DownloadArte(int id)
        {
            var arte = await _context.ArquivoArtes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ArquivoId == id);

            if (arte == null) return NotFound();
            if (string.IsNullOrWhiteSpace(arte.CaminhoFisico)) return NotFound(new { erro = "Sem caminho físico salvo." });
            if (!System.IO.File.Exists(arte.CaminhoFisico)) return NotFound(new { erro = "Arquivo não encontrado no servidor." });

            var contentType = string.IsNullOrWhiteSpace(arte.ContentType) ? "application/octet-stream" : arte.ContentType;
            var bytes = await System.IO.File.ReadAllBytesAsync(arte.CaminhoFisico);

            // baixa com o nome original
            return File(bytes, contentType, arte.NomeArquivo);
        }

        [Authorize(Roles = "God,Admin,Arte,Vendedor")]
        [HttpPost("UploadArte")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(80 * 1024 * 1024)]
        public async Task<IActionResult> UploadArte([FromForm] UploadArteRequest req)
        {
            if (req.ItemId <= 0) return BadRequest(new { erro = "Item inválido." });
            if (req.File == null || req.File.Length == 0) return BadRequest(new { erro = "Arquivo inválido." });

            var itemId = req.ItemId;
            var file = req.File;

            var ext = Path.GetExtension(file.FileName);
            var permitidas = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".pdf", ".cdr", ".zip", ".png", ".jpg", ".jpeg" };
            if (!permitidas.Contains(ext)) return BadRequest(new { erro = $"Extensão não permitida: {ext}" });

            var root = _config["ArteStorage:RootPath"];
            if (string.IsNullOrWhiteSpace(root))
                return StatusCode(500, new { erro = "ArteStorage:RootPath não configurado." });

            var pasta = Path.Combine(root, $"Item-{itemId}");
            Directory.CreateDirectory(pasta);

            var nomeOriginal = Path.GetFileName(file.FileName);
            var nomeFinal = $"{itemId}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{ext}";
            var caminhoFisico = Path.Combine(pasta, nomeFinal);

            await using (var fs = System.IO.File.Create(caminhoFisico))
                await file.CopyToAsync(fs);

            const int STATUS_ENVIADA = 2;
            const string PLACEHOLDER_URL = "/api/Producao/DownloadArte/0"; // ✅ nunca nulo

            var existente = await _context.ArquivoArtes.FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (existente != null)
            {
                existente.NomeArquivo = nomeOriginal;
                existente.CaminhoArquivo = PLACEHOLDER_URL;  // ✅ nunca nulo
                existente.CaminhoFisico = caminhoFisico;
                existente.StatusArteId = STATUS_ENVIADA;
                existente.ContentType = file.ContentType;
                existente.TamanhoBytes = file.Length;
                existente.UsuarioUpload = User.Identity?.Name;
                existente.DataUpload = DateTime.UtcNow;
            }
            else
            {
                existente = new ArquivoArte
                {
                    ItemId = itemId,
                    NomeArquivo = nomeOriginal,
                    CaminhoArquivo = PLACEHOLDER_URL,          // ✅ nunca nulo
                    CaminhoFisico = caminhoFisico,
                    StatusArteId = STATUS_ENVIADA,
                    ContentType = file.ContentType,
                    TamanhoBytes = file.Length,
                    UsuarioUpload = User.Identity?.Name,
                    DataUpload = DateTime.UtcNow
                };
                _context.ArquivoArtes.Add(existente);
            }

            await _context.SaveChangesAsync(); // ✅ agora passa

            existente.CaminhoArquivo = $"/api/Producao/DownloadArte/{existente.ArquivoId}";
            await _context.SaveChangesAsync();

            // ... (código existente que salva o arquivo)
            await _context.SaveChangesAsync(); // A linha que já existe aí

            // 🚀 NOVO: Avisa a OS que a arte chegou para o vendedor aprovar!
            var itemPedido = await _context.PedidoItems.FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (itemPedido != null)
            {
                var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.PedidoId == itemPedido.PedidoId);
                if (pedido != null && pedido.StatusId < 3)
                {
                    pedido.StatusId = 3; // 3 = Arte em Análise
                    await _context.SaveChangesAsync();
                }
            }


            return Ok(new
            {
                id = existente.ArquivoId,
                itemId = existente.ItemId,
                nomeArquivo = existente.NomeArquivo,
                downloadUrl = existente.CaminhoArquivo,
                statusArteId = existente.StatusArteId
            });
        }

        [HttpPost("UploadFotoArte/{itemId}")]
        public async Task<IActionResult> UploadFotoArte(int itemId, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Arquivo não enviado");

            // 1. Caminho físico onde o Docker/Windows salva o arquivo
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "fotos");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = $"foto_{itemId}.jpg"; // Nome fixo por ID para facilitar
            var filePath = Path.Combine(uploadsFolder, fileName);

            // 2. Salva o arquivo (FileMode.Create substitui se já existir)
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 3. ATUALIZA O BANCO: O segredo está na barra inicial '/'
            var item = await _context.PedidoItems.FirstOrDefaultAsync(p => p.ItemId == itemId);
            if (item != null)
            {
                // 🚀 SALVE ASSIM: Começando com / para o navegador achar a raiz
                item.CaminhoFoto = $"/uploads/fotos/{fileName}";
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [Authorize(Roles = "God,Admin,Arte")]
        [HttpPut("VincularArquivoArte")]
        public async Task<IActionResult> VincularArquivoArte([FromBody] VincularArquivoArteInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Vincular_Arquivo_Arte 
                        @Item_ID = {model.ItemID}, 
                        @Nome_Arquivo = {model.NomeArquivo}, 
                        @Caminho_Arquivo = {model.CaminhoArquivo}, 
                        @Usuario_ID = {model.UsuarioID}");
                return Ok(new { mensagem = "Arquivo de arte vinculado ao item com sucesso!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao vincular arquivo de arte", detalhe = ex.Message });

            }

        }

        [Authorize(Roles = "God,Admin,Arte")]
        [HttpPut("AtualizarStatusArte")]
        public async Task<IActionResult> AtualizarStatusArte([FromBody] AtualizarStatusArteInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $@"EXEC sp_set_session_context @key = N'UsuarioId', @value = {model.UsuarioId};");

                await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC SP_Atualizar_Status_Arte 
                @Item_ID = {model.ItemId}, 
                @Novo_Status_Arte_ID = {model.NovoStatusArteId}, 
                @Usuario_ID = {model.UsuarioId}");

                return Ok(new { mensagem = "Status da arte atualizado!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro no SQL da Arte", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Arte")]
        [HttpGet("StatusArteListar")]
        public async Task<IActionResult> StatusArteListar()
        {
            var lista = await _context.StatusArtes
                .OrderBy(s => s.StatusArteId)
                .Select(s => new StatusOption { Id = s.StatusArteId, Nome = s.Nome })
                .ToListAsync();

            return Ok(lista);
        }

    }
}