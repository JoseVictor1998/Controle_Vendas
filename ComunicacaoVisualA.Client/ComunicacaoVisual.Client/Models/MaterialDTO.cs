namespace ComunicacaoVisual.Client.Models
{
    public class MaterialDTO
    {
        public int MaterialId { get; set; }
        public string Nome { get; set; } = "";
        public string Descricao { get; set; } = "";
        public bool Ativo { get; set; }
    }
}