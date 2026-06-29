using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;

public record ListarTarefaDto(
    Guid Id,
    string Titulo,
    Prioridade Prioridade,
    DateTime DataDeCriacao,
    DateTime DataDeConclusao,
    StatusDeConclusao StatusDeConclusao,
    double PercentualConcluido
);

public record CadastrarTarefaDto(
    string Titulo,
    Prioridade Prioridade,
    DateTime DataDeCriacao,
    DateTime DataDeConclusao,
    StatusDeConclusao StatusDeConclusao
);

public record EditarTarefaDto(
    Guid Id,
    string Titulo,
    Prioridade Prioridade,
    DateTime DataDeCriacao,
    DateTime DataDeConclusao,
    StatusDeConclusao StatusDeConclusao
);

public record DetalhesTarefaDto(
    Guid Id,
    string Titulo,
    Prioridade Prioridade,
    DateTime DataDeCriacao,
    DateTime DataDeConclusao,
    StatusDeConclusao StatusDeConclusao,
    double PercentualConcluido
);