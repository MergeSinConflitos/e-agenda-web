using System;
using AutoMapper;

namespace eAgendaWeb.WebApplication.ModuloTarefa.Apresentacao;

public class TarefaProfile : Profile
{
    public TarefaProfile()
    {
        CreateMap<ListarTarefaDto, ListarTarefaViewModel>();
        CreateMap<CadastrarTarefaViewModel, CadastrarTarefaDto>();
        CreateMap<EditarTarefaViewModel, EditarTarefaDto>();
        CreateMap<DetalhesTarefaDto, EditarTarefaViewModel>();
        CreateMap<DetalhesTarefaDto, ExcluirTarefaViewModel>();
    }
}
