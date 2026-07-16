namespace Domain.Entities
{
    public class Banco
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public decimal PercentualJuros { get; set; }

        public ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();
    }
}
