using AutoMapper;
using eAgendaWeb.WebApplication.ModuloContatos.Aplicacao;

namespace eAgendaWeb.WebApplication.ModuloContatos.Apresentacao;

public class ContatoProfile : Profile
{
    public ContatoProfile()
    {
        CreateMap<ListarContatosDto, ListarContatosViewModel>();
        CreateMap<CadastrarContatosViewModel, CadastrarContatoDto>();
        CreateMap<EditarContatoViewModel, EditarContatoDto>();
        CreateMap<DetalhesContatoDto, EditarContatoViewModel>();
        CreateMap<DetalhesContatoDto, ExcluirContatoViewModel>();
    }
}