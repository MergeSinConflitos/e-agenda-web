using System;
using Dapper;
using eAgendaWeb.WebApplication.Compartilhado.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloItem.Dominio;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;
using Microsoft.Data.SqlClient;

namespace eAgendaWeb.WebApplication.ModuloItem.Infra.SQL;

public class RepositorioItemEmSql(ISqlConnectionFactory connectionFactory)
    : RepositorioBaseSql<Item>(connectionFactory), IRepositorioItem
{
    protected override string InserirSql => """
    INSERT INTO dbo.TBItem
    (
        Id,
        Titulo,
        StatusDeConclusao
    )
    VALUES
    (
        @Id,
        @Titulo,
        @StatusDeConclusao
    );
    """;
    protected override string AtualizarSql => """
        UPDATE dbo.TBItem
        SET
            Titulo = @Titulo,
            StatusDeConclusao = @StatusDeConclusao
        WHERE Id = @Id;
        """;

    protected override string ExcluirSql => """
        DELETE FROM dbo.TBItem
        WHERE Id = @Id;
        """;

    protected override string SelecionarPorIdSql => """
    SELECT
        i.Id AS ItemId,
        i.Titulo,
        i.StatusDeConclusao,

        t.Id AS TarefaId,
        t.Titulo AS TarefaTitulo

    FROM dbo.TBItem i

    INNER JOIN dbo.TBItemTarefa it
        ON it.ItemId = i.Id

    INNER JOIN dbo.TBTarefa t
        ON t.Id = it.TarefaId

    WHERE i.Id = @Id;
    """;

    protected override string SelecionarTodosSql => """
    SELECT
        i.Id AS ItemId,
        i.Titulo,
        i.StatusDeConclusao,

        t.Id AS TarefaId,
        t.Titulo AS TarefaTitulo

    FROM dbo.TBItem i

    INNER JOIN dbo.TBItemTarefa it
        ON it.ItemId = i.Id

    INNER JOIN dbo.TBTarefa t
        ON t.Id = it.TarefaId;
    """;

    public override void Cadastrar(Item item)
    {
        using SqlConnection conexao = AbrirConexao();

        conexao.Execute(
            InserirSql,
            CriarParametros(item)
        );

        conexao.Execute("""
        INSERT INTO dbo.TBItemTarefa
        (
            Id,
            ItemId,
            TarefaId
        )
        VALUES
        (
            @Id,
            @ItemId,
            @TarefaId
        );
    """,
        new
        {
            Id = Guid.NewGuid(),
            ItemId = item.Id,
            TarefaId = item.TarefaId
        });
    }

    public override bool Excluir(Guid id)
    {
        using SqlConnection conexao = AbrirConexao();

        conexao.Execute("""
        DELETE FROM dbo.TBItemTarefa
        WHERE ItemId = @Id;
    """,
        new { Id = id });


        return conexao.Execute(
            ExcluirSql,
            new { Id = id }
        ) == 1;
    }

    public override Item? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = AbrirConexao();

        ItemRow? row = conexao.QueryFirstOrDefault<ItemRow>(
            SelecionarPorIdSql,
            new { Id = idSelecionado });

        if (row == null)
            return null;

        return MapearItem(row);
    }

    public override List<Item> SelecionarTodos()
    {
        using SqlConnection conexao = AbrirConexao();

        return conexao
            .Query<ItemRow>(SelecionarTodosSql)
            .Select(MapearItem)
            .ToList();
    }

    private static Item MapearItem(ItemRow row)
    {
        return new Item
        {
            Id = row.ItemId,
            Titulo = row.Titulo,
            StatusDeConclusao = row.StatusDeConclusao,
            TarefaId = row.TarefaId,

            Tarefa = new Tarefa
            {
                Id = row.TarefaIdRelacionada,
                Titulo = row.TarefaTitulo
            }
        };
    }

    protected override object CriarParametros(Item item)
    {
        return new
        {
            item.Id,
            item.Titulo,
            StatusDeConclusao = (int)item.StatusDeConclusao
        };
    }
}

public sealed class ItemRow
{
    public Guid ItemId { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public StatusDeConclusao StatusDeConclusao { get; set; }

    public Guid TarefaId { get; set; }

    public Guid TarefaIdRelacionada { get; set; }

    public string TarefaTitulo { get; set; } = string.Empty;
}
