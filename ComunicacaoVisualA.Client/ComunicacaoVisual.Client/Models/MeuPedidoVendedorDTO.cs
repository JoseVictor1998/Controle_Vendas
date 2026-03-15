namespace ComunicacaoVisual.Client.Models
{
    // 💰 CLASSES DO FINANCEIRO (CUSTOS)
    public class CustoFixoDTO
    {
        public int CustoId { get; set; }
        public string Descricao { get; set; } = "";
        public decimal Valor { get; set; }
        public DateOnly DataVencimento { get; set; }
        public bool? StatusPagamento { get; set; }
    }

    public class CustoFixoInput
    {
        public string Descricao { get; set; } = "";
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; } = DateTime.Now;
        public bool StatusPagamento { get; set; } = false;
    }
}
