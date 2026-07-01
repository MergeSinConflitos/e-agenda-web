using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;

namespace eAgendaWeb.WebApplication.ModuloCategoria.Infra.SQL;

public class RepositorioCategoriaEmSql(ISqlConnectionFactory connectionFactory) :
    RepositorioBaseSql<Categoria>(connectionFactory), IRepositorioCategoria
{
    protected override string InserirSql => """
        INSERT INTO dbo.TBCategoria (Id, Titulo)
        VALUES (@Id, @Titulo);
    """;

    protected override string AtualizarSql => """
        UPDATE dbo.TBCategoria
        SET
            Titulo = @Titulo
        WHERE Id = @Id;
    """;

    protected override string ExcluirSql => """
        DELETE FROM dbo.TBCategoria
        WHERE Id = @Id;
    """;

    protected override string SelecionarPorIdSql => """
        SELECT Id, Titulo
        FROM dbo.TBCategoria
        WHERE Id = @Id;
    """;

    protected override string SelecionarTodosSql => """
        SELECT Id,Titulo
        FROM dbo.TBCategoria
        ORDER BY Titulo;
    """;
}