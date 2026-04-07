namespace ComunicacaoVisual.Client.Models
{
    public class VwPesquisaClientesVendaModel
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Documento { get; set; }

        public string? TipoCliente { get; set; }

        public string Telefone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Localidade { get; set; } = string.Empty;

        public bool Ativo { get; set; }
    }
}