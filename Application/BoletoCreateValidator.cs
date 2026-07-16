using Application.DTOs;
using FluentValidation;

namespace Application
{
    public class BoletoCreateValidator : AbstractValidator<BoletoCreateDto>
    {
        public BoletoCreateValidator()
        {
            RuleFor(x => x.NomePagador).NotEmpty().WithMessage("Nome do pagador é obrigatório.");
            RuleFor(x => x.CpfCnpjPagador).NotEmpty().Length(11, 14).WithMessage("CPF/CNPJ inválido.");
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
            RuleFor(x => x.BancoId).NotEmpty().WithMessage("O BancoId é obrigatório.");
            RuleFor(x => x.DataVencimento).NotEmpty().WithMessage("Data de vencimento obrigatória.");
        }
    }
}
