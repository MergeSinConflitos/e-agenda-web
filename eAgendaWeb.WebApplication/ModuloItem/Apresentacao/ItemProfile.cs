using System;
using AutoMapper;
using eAgendaWeb.WebApplication.ModuloItem.Aplicacao;

namespace eAgendaWeb.WebApplication.ModuloItem.Apresentacao;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<OpcaoTarefaDto, OpcaoTarefaViewModel>();
        CreateMap<ListarItemDto, ListarItemViewModel>();
        CreateMap<DetalhesItemDto, ExcluirItemViewModel>();
        CreateMap<CadastrarItemViewModel, CadastrarItemDto>();
    }
}
