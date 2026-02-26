namespace ComunicacaoVisual.API.DBModels
{
    public class AtualizarStatusArteInput
    {
        public int ItemId { get; set; }
        public int NovoStatusArteId { get; set; } // 3=Em Correção, 4=Aprovada, 5=Reprovada
    }
}