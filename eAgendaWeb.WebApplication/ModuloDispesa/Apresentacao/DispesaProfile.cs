using System;
using AutoMapper;
using eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Apresentacao;

public class DispesaProfile : Profile
{
    public DispesaProfile()
    {
        CreateMap<OpcaoCategoriaDto, OpcaoCategoriaViewModel>();

        CreateMap<ListarDispesaDto, ListarDispesaViewModel>();

        CreateMap<CadastrarDispesaViewModel, CadastrarDispesaDto>();

        CreateMap<EditarDispesaViewModel, EditarDispesaDto>();

        CreateMap<DetalhesDispesaDto, EditarDispesaViewModel>()
            .ForCtorParam("Categorias", opt => opt.MapFrom(_ => new List<OpcaoCategoriaViewModel>()));

        CreateMap<DetalhesDispesaDto, ExcluirDispesaViewModel>();
    }
}