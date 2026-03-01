namespace ComunicacaoVisual.API.DBModels
{
    public class AtualizarStatusInput
    {
        public int ItemId { get; set; }
        public int NovoStatusId { get; set; }
        public int UsuarioId { get; set; }
    }
}