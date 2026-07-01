using System;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloItem.Aplicacao;

public record ListarItemDto(
    Guid Id,
    string Titulo,
    StatusDeConclusao StatusDeConclusao,
    Guid TarefaId,
    string TarefaTitulo
);

public record CadastrarItemDto(
    string Titulo,
    StatusDeConclusao StatusDeConclusao,
    Guid TarefaId
);

public record DetalhesItemDto(
    Guid Id,
    string Titulo,
    StatusDeConclusao StatusDeConclusao,
    Guid TarefaId,
    string TarefaTitulo
);

public record OpcaoTarefaDto(
    Guid Id,
    string Titulo
);