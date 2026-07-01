using AutoMapper;
using eAgendaWeb.WebApplication.ModuloCompromisso.Aplicacao;

namespace eAgendaWeb.WebApplication.ModuloCompromisso.Apresentacao;

public class CompromissoProfile : Profile
{
    public CompromissoProfile()
    {
        CreateMap<OpcaoContatoDto, OpcaoContatoViewModel>();
        CreateMap<ListarCompromissoDto, ListarCompromissosViewModel>();
        CreateMap<CadastrarCompromissoViewModel, CadastrarCompromissoDto>();
        CreateMap<EditarCompromissoViewModel, EditarCompromissoDto>();

        CreateMap<DetalhesCompromissoDto, EditarCompromissoViewModel>()
            .ForCtorParam("Contatos", opt => opt.MapFrom(_ => new List<OpcaoContatoViewModel>()));

        CreateMap<DetalhesCompromissoDto, ExcluirCompromissoViewModel>();
    }
}
