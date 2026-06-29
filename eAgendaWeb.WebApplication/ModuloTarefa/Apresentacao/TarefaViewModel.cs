using System.ComponentModel.DataAnnotations;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloTarefa.Apresentacao;

public record ListarTarefaViewModel(
    Guid Id,
    string Titulo,
    Prioridade Prioridade,
    DateTime DataDeCriacao,
    DateTime DataDeConclusao,
    StatusDeConclusao StatusDeConclusao,
    double PercentualConcluido
);

public record CadastrarTarefaViewModel(
    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres")]
    string Titulo,

    [Required(ErrorMessage = "Selecione uma prioridade")]
    Prioridade Prioridade,

    [Required(ErrorMessage = "Informe a data de criação")]
    [DataType(DataType.Date)]
    DateTime DataDeCriacao
);

public record EditarTarefaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres")]
    string Titulo,

    [Required(ErrorMessage = "Selecione uma prioridade")]
    Prioridade Prioridade,

    [Required(ErrorMessage = "Informe a data de criação")]
    [DataType(DataType.Date)]
    DateTime DataDeCriacao
);

public record ExcluirTarefaViewModel(
     Guid Id,
    string Titulo,
    Prioridade Prioridade,
    DateTime DataDeCriacao,
    DateTime DataDeConclusao,
    StatusDeConclusao StatusDeConclusao,
    double PercentualConcluido
);