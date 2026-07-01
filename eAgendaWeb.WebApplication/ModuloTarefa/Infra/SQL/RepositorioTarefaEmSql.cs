using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloTarefa.Infra.SQL;

public class RepositorioTarefaEmSql(ISqlConnectionFactory connectionFactory) :
    RepositorioBaseSql<Tarefa>(connectionFactory), IRepositorioTarefa
{
    protected override string InserirSql => """
        INSERT INTO dbo.TBTarefa
            (Id, Titulo, Prioridade, DataDeCriacao, DataDeConclusao, StatusDeConclusao, PercentualConcluido)
        VALUES
            (@Id, @Titulo,  @Prioridade, @DataDeCriacao, @DataDeConclusao, @StatusDeConclusao, @PercentualConcluido);
    """;


    protected override string AtualizarSql => """
    UPDATE dbo.TBTarefa
    SET
        Titulo = @Titulo,
        Prioridade = @Prioridade,
        DataDeCriacao = @DataDeCriacao,
        DataDeConclusao = @DataDeConclusao,
        StatusDeConclusao = @StatusDeConclusao,
        PercentualConcluido = @PercentualConcluido
    WHERE
        Id = @Id;
    """;

    protected override string ExcluirSql => """
    DELETE FROM dbo.TBTarefa
    WHERE
        Id = @Id;
    """;
    protected override string SelecionarPorIdSql => """
    SELECT
        Id,
        Titulo,
        Prioridade,
        DataDeCriacao,
        DataDeConclusao,
        StatusDeConclusao,
        PercentualConcluido
    FROM dbo.TBTarefa
    WHERE Id = @Id;
    """;

    protected override string SelecionarTodosSql => """
    SELECT
        Id,
        Titulo,
        Prioridade,
        DataDeCriacao,
        DataDeConclusao,
        StatusDeConclusao,
        PercentualConcluido
    FROM dbo.TBTarefa;
    """;
}
