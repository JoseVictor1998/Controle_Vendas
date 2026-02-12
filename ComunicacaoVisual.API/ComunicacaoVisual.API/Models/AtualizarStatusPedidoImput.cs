namespace ComunicacaoVisual.API.Models
{
    public class AtualizarStatusPedidoImput
    {
        public int PedidoId { get; set; }
        public int NovoStatusId { get; set; }
        public int UsuarioId { get; set; }
        public decimal? ValorTotal { get; set; } = null;
        public string FormaPagamento { get; set; } = null!;

    }
}
