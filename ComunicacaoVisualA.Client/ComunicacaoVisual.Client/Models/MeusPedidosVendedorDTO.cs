using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComunicacaoVisual.Client.Models
{
    public class MeuPedidoVendedorDTO
    {
        [JsonPropertyName("pedido_ID")]
        public int PedidoId { get; set; }

        [JsonPropertyName("os")]
        public string Os { get; set; } = "";

        [JsonPropertyName("cliente")]
        public string Cliente { get; set; } = "";

        [JsonPropertyName("status_ID")]
        public int StatusId { get; set; }

        [JsonPropertyName("status_Atual")]
        public string StatusAtual { get; set; } = "";

        [JsonPropertyName("valor_Total")]
        public decimal? ValorTotal { get; set; }

        [JsonPropertyName("data_Pedido")]
        public DateTime? DataPedido { get; set; }

        [JsonPropertyName("qtd_Itens")]
        public int QtdItens { get; set; }

        [JsonPropertyName("observacao_Geral")]
        public string? ObservacaoGeral { get; set; }
    }

    public class AtualizarStatusPedidoInput
    {
        public int PedidoId { get; set; }
        public int NovoStatusId { get; set; }
        public int UsuarioId { get; set; }
        public decimal? ValorTotal { get; set; }
        public string? FormaPagamento { get; set; }
        public int? Parcelas { get; set; }
    }

    // 🚀 TUDO DAQUI PRA BAIXO É NOVO: Para o Modal de Correção funcionar perfeitamente!
    public class CorrecaoCompletaInput
    {
        public int PedidoId { get; set; }
        public int UsuarioId { get; set; }
        public string NovaObservacao { get; set; } = "";
        public List<ItemDetalhadoDto> Itens { get; set; } = new();
    }

    public class TipoProdutoOption
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
    }

    public class PedidoDetalhadoDto
    {
        public int PedidoId { get; set; }
        public string OsExterna { get; set; } = "";
        public DateTime? DataPedido { get; set; }
        public string ObservacaoGeral { get; set; } = "";
        public decimal? ValorTotal { get; set; }
        public string FormaPagamento { get; set; } = "";
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
        public string? TipoProdutoNome { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string? ObservacaoTecnica { get; set; }
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