namespace ComunicacaoVisual.API.DBModels
{
    public class AtualizarStatusInput
    {
        public int PedidoId { get; set; }
        public int NovoStatusId { get; set; }
        public int UsuarioId { get; set; }
    }
}