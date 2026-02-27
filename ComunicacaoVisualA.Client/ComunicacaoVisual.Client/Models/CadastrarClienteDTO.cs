namespace ComunicacaoVisual.Client.Models
{
    public class CadastrarClienteDTO
    {

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DDD { get; set; } = string.Empty;

        public string NumeroTelefone { get; set; } = string.Empty;

        public string Cidade { get; set; } = string.Empty;

        public string CEP { get; set; } = string.Empty;

        public string Bairro { get; set; } = string.Empty;

        public string Rua { get; set; } = string.Empty;

        public int NumeroEndereco { get; set; }

        public string Documento { get; set; } = string.Empty;

        public string Tipo { get; set; } = "PF";

    }
}