using System.Text.Json.Serialization;

namespace ComunicacaoVisual.Client.Models
{
    
    public class CriarPedidoComItemInput
    {
        public int ClienteId { get; set; }
        public string OsExterna { get; set; } = "";
        public int VendedorID { get; set; }
        public string? ObservacaoGeral { get; set; }

        //  Uma lista de itens!
        public List<NovoItemDTO> Itens { get; set; } = new List<NovoItemDTO>();
    }

    // O Detalhe (Cada material dentro do pedido)
    public class NovoItemDTO
    {
        public int TipoProdutoId { get; set; }

        // Vamos guardar o nome só para exibir na tela de confirmação (não precisa ir para o banco)
        public string NomeProdutoSelecionado { get; set; } = "";

        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; } = 1;
        public string? ObservacaoTecnica { get; set; }
        public string? CaminhoFoto { get; set; }
        [JsonIgnore] public string? PreviewUrl { get; set; }
        [JsonIgnore] public string? FileName { get; set; }
        [JsonIgnore] public bool FazendoUpload { get; set; }
    }
}