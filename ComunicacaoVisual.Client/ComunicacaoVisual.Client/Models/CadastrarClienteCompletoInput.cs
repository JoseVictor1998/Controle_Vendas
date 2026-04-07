namespace ComunicacaoVisual.Client.Models
{
    public class CadastrarClienteCompletoInput
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DDD { get; set; } = string.Empty;
        public string NumeroTelefone { get; set; } = string.Empty;

        public string Cidade { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Rua { get; set; } = string.Empty;
        public string NumeroEndereco { get; set; } = string.Empty;

        public string Documento { get; set; } = string.Empty; // CPF/CNPJ
        public string Tipo { get; set; } = "PF"; // PF ou PJ
    }
}