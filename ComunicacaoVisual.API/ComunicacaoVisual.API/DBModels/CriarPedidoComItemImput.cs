namespace ComunicacaoVisual.API.DBModels
{
    // Substitua a sua classe antiga por esta no Back-end
    public class CriarPedidoComItemInput
    {
        public int ClienteId { get; set; }
        public string OsExterna { get; set; } = "";
        public int VendedorID { get; set; }
        public string? ObservacaoGeral { get; set; }

        // 🚀 A API AGORA RECEBE A LISTA DE ITENS
        public List<NovoItemDTO> Itens { get; set; } = new List<NovoItemDTO>();
    }

    public class NovoItemDTO
    {
        public int TipoProdutoId { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string? ObservacaoTecnica { get; set; }
        public string? CaminhoFoto { get; set; }
    }

}
