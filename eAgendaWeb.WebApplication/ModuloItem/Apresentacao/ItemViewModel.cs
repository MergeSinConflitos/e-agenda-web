using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;


public record ListarItemViewModel(
    Guid Id,
    string Titulo,
    StatusDeConclusao StatusDeConclusao,
    Guid TarefaId,
    string TarefaTitulo
);


public record CadastrarItemViewModel(
    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres.")]
    string Titulo,

    [Required(ErrorMessage = "A seleção de \"Tarefa\" é obrigatória.")]
    Guid TarefaId,

    StatusDeConclusao StatusDeConclusao,

    [ValidateNever]
    List<OpcaoTarefaViewModel> Tarefas
);


public record OpcaoTarefaViewModel(
    Guid Id,
    string Titulo
);


public record ExcluirItemViewModel(
    Guid Id,
    string Titulo,
    StatusDeConclusao StatusDeConclusao,
    Guid TarefaId,
    string TarefaTitulo
);


public record GerenciarItemViewModel(
    Guid TarefaId,
    string TarefaTitulo,
    List<ListarItemViewModel> Itens
);