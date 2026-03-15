namespace ComunicacaoVisual.Client.Models
{
    public class CadastrarUsuarioInput
    {
        public string Nome { get; set; } = "";
        public string Login { get; set; } = "";
        public string Senha { get; set; } = "";
        public string Nivel_Acesso { get; set; } = "Vendedor"; // Padrão
    }
}