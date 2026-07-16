using AutoMapper;
using Domain.Entities;
using global::Application.DTOs;

namespace Application.Mappings
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BancoCreateDto, Banco>();
            CreateMap<Banco, BancoResponseDto>();

            CreateMap<BoletoCreateDto, Boleto>();

            CreateMap<Boleto, BoletoResponseDto>()
                .ForMember(dest => dest.ValorOriginal, opt => opt.MapFrom(src => src.Valor))
                .ForMember(dest => dest.ValorCobrado, opt => opt.MapFrom(src => src.CalcularValorComJuros(DateTime.Now)))
                .ForMember(dest => dest.EstaVencido, opt => opt.MapFrom(src => DateTime.Now.Date > src.DataVencimento.Date))
                .ForMember(dest => dest.NomeBanco, opt => opt.MapFrom(src => src.Banco!.Nome))
                .ForMember(dest => dest.CodigoBanco, opt => opt.MapFrom(src => src.Banco!.Codigo));
        }
    }
}