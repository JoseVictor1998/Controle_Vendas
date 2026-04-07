using System;

namespace ComunicacaoVisual.Client.Models
{
    public class BuscaRapidaDTO
    {
        public string Os { get; set; } = string.Empty;

        public string Nome { get; set; } = string.Empty;

        public DateTime? DataPedido { get; set; }

        public string StatusAtual { get; set; } = string.Empty;

        public string Produto { get; set; } = string.Empty;

        public decimal Altura { get; set; }

        public decimal Largura { get; set; }

        public int Quantidade { get; set; }
    }
}