namespace ComunicacaoVisual.API.Models
{
    public class CadastrarClienteCompletoInput
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DDD { get; set; } = null!;
        public string NumeroTelefone { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string CEP { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Rua { get; set; } = null!;
        public string NumeroEndereco { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public string Tipo { get; set; } = null!;

    }
}
