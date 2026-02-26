namespace ComunicacaoVisual.API.DBModels
{
    public class PedidoDetalhadoDto
    {
        public int PedidoId { get; set; }
        public string OsExterna { get; set; } = "";
        public DateTime? DataPedido { get; set; }
        public string ObservacaoGeral { get; set; } = "";
        public decimal? ValorTotal { get; set; }
        public string? FormaPagamento { get; set; }

        public int StatusId { get; set; }
        public string StatusNome { get; set; } = "";

        public ClienteResumoDto Cliente { get; set; } = new();
        public List<ItemDetalhadoDto> Itens { get; set; } = new();
    }

    public class ClienteResumoDto
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
    }

    public class ItemDetalhadoDto
    {
        public int ItemId { get; set; }
        public int TipoProdutoId { get; set; }
        public string TipoProdutoNome { get; set; } = "";
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string ObservacaoTecnica { get; set; } = "";
        public string? CaminhoFoto { get; set; }

        public ArquivoArteDto? Arte { get; set; }
    }

    public class ArquivoArteDto
    {
        public int ArquivoId { get; set; }
        public string NomeArquivo { get; set; } = "";
        public string CaminhoArquivo { get; set; } = "";
        public int StatusArteId { get; set; }
        public string StatusArteNome { get; set; } = "";
    }
}