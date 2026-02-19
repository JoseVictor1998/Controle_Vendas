namespace ComunicacaoVisual.API.DBModels
{
    public class VincularArquivoArteInput
    {
        public int ItemID { get; set; }
        public string NomeArquivo { get; set; } = null!;
        public string CaminhoArquivo { get; set; } = null!;
        public int UsuarioID { get; set; }

    }
}

