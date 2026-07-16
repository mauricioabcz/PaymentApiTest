namespace Domain.Entities
{

    public class Boleto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NomePagador { get; set; } = string.Empty;
        public string CpfCnpjPagador { get; set; } = string.Empty;
        public string NomeBeneficiario { get; set; } = string.Empty;
        public string CpfCnpjBeneficiario { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public string? Observacao { get; set; }

        public Guid BancoId { get; set; }
        public Banco? Banco { get; set; }

        public decimal CalcularValorComJuros(DateTime dataConsulta)
        {
            if (dataConsulta.Date > DataVencimento.Date && Banco != null)
            {
                decimal juros = Valor * (Banco.PercentualJuros / 100m);
                return Valor + juros;
            }
            return Valor;
        }
    }
}
