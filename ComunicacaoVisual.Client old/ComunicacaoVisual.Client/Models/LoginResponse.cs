namespace ComunicacaoVisual.Client.Models
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}