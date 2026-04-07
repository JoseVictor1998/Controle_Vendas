namespace FrontBase44.DTOs
{
    public class ClienteDTO
    {

        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string Documento { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DDD { get; set; } = string.Empty;

        public string NumeroTelefone { get; set; } = string.Empty;

        public string Cidade { get; set; } = string.Empty;

        public bool Ativo { get; set; }

    }
}
