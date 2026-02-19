namespace ComunicacaoVisual.API.DBModels
{
    public class UsuarioLoginDTO
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
        public string NivelAcesso { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
