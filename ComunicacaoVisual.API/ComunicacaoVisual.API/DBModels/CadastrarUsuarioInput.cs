namespace ComunicacaoVisual.API.DBModels
{
    public class CadastrarUsuarioInput
    {
        public string Nome { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Nivel_Acesso { get; set; } = "Vendedor";
    }
}