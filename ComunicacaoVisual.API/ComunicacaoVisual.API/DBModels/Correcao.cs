namespace ComunicacaoVisual.API.DBModels
{
    public class SolicitarCorrecaoInput
    {
        public int PedidoId { get; set; }
        public int UsuarioId { get; set; }
        public int NovoStatusId { get; set; }
        public string NovaObservacao { get; set; } = "";
    }
    public class CorrecaoCompletaInput
    {
        public int PedidoId { get; set; }
        public int UsuarioId { get; set; }
        public string NovaObservacao { get; set; } = "";

        // Como a classe já existe no seu projeto, o C# vai achar ela sozinho!
        public List<ItemDetalhadoDto> Itens { get; set; } = new();
    }

}
