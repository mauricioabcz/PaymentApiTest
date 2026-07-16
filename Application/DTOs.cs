namespace Application.DTOs
{
    public record BancoCreateDto(string Nome, string Codigo, decimal PercentualJuros);
    public record BancoResponseDto(Guid Id, string Nome, string Codigo, decimal PercentualJuros);

    public record BoletoCreateDto(
        string NomePagador, string CpfCnpjPagador,
        string NomeBeneficiario, string CpfCnpjBeneficiario,
        decimal Valor, DateTime DataVencimento,
        string? Observacao, Guid BancoId
    );

    public record BoletoResponseDto(
        Guid Id, string NomePagador, string CpfCnpjPagador,
        string NomeBeneficiario, string CpfCnpjBeneficiario,
        decimal ValorOriginal, decimal ValorCobrado,
        DateTime DataVencimento, string? Observacao,
        string NomeBanco, string CodigoBanco, bool EstaVencido
    );
}