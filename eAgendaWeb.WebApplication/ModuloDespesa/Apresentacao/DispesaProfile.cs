using System;
using AutoMapper;
using eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Apresentacao;

public class DespesaProfile : Profile
{
    public DespesaProfile()
    {
        CreateMap<OpcaoCategoriaDto, OpcaoCategoriaViewModel>();

        CreateMap<ListarDespesaDto, ListarDespesaViewModel>();

        CreateMap<CadastrarDespesaViewModel, CadastrarDespesaDto>();

        CreateMap<EditarDespesaViewModel, EditarDespesaDto>();

        CreateMap<DetalhesDespesaDto, EditarDespesaViewModel>()
            .ForCtorParam("Categorias", opt => opt.MapFrom(_ => new List<OpcaoCategoriaViewModel>()));

        CreateMap<DetalhesDespesaDto, ExcluirDespesaViewModel>();
    }
}