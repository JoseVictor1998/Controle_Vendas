namespace ComunicacaoVisual.API.DBModels
{
    // 📦 CAIXINHAS DO PRODUTO COMPLETO
    public class CadastrarTipoProdutoInput
    {
        public int Categoria_ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao_Tecnica { get; set; } = string.Empty;
        public bool Usa_Adesivo { get; set; }
        public bool Usa_Mascara { get; set; }
        public int Material_ID_1 { get; set; }
        public int? Material_ID_2 { get; set; }
        public int? Material_ID_3 { get; set; }
    }

    public class CategoriaOption
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
