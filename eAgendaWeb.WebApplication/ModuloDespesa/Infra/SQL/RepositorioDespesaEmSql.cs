using System;
using Dapper;
using eAgendaWeb.WebApplication.Compartilhado.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using Microsoft.Data.SqlClient;

namespace eAgendaWeb.WebApplication.ModuloDespesa.Infra.SQL;

public class RepositorioDespesaEmSql(ISqlConnectionFactory connectionFactory) :
    RepositorioBaseSql<Despesa>(connectionFactory), IRepositorioDespesa
{
    protected override string InserirSql => """
        INSERT INTO dbo.TBDespesa
            (Id, Descricao, DataDeOcorrencia, Valor, FormaDePagamento)
        VALUES
            (@Id, @Descricao, @DataDeOcorrencia, @Valor, @FormaDePagamento);
    """;


    protected override string AtualizarSql => """
        UPDATE dbo.TBDespesa
        SET
            Descricao = @Descricao,
            DataDeOcorrencia = @DataDeOcorrencia,
            Valor = @Valor,
            FormaDePagamento = @FormaDePagamento
        WHERE Id = @Id;
    """;


    protected override string ExcluirSql => """
        DELETE FROM dbo.TBDespesa
        WHERE Id = @Id;
    """;


    protected override string SelecionarPorIdSql => """
        SELECT
            dp.Id AS DespesaId,
            dp.Descricao,
            dp.DataDeOcorrencia,
            dp.Valor,
            dp.FormaDePagamento,

            ct.Id AS CategoriaId,
            ct.Titulo AS CategoriaTitulo

        FROM dbo.TBDespesa dp

        LEFT JOIN dbo.TBDespesaCategoria dc
            ON dc.DespesaId = dp.Id

        LEFT JOIN dbo.TBCategoria ct
            ON ct.Id = dc.CategoriaId

        WHERE dp.Id = @Id;
    """;


    protected override string SelecionarTodosSql => """
        SELECT
            dp.Id AS DespesaId,
            dp.Descricao,
            dp.DataDeOcorrencia,
            dp.Valor,
            dp.FormaDePagamento,

            ct.Id AS CategoriaId,
            ct.Titulo AS CategoriaTitulo

        FROM dbo.TBDespesa dp

        LEFT JOIN dbo.TBDespesaCategoria dc
            ON dc.DespesaId = dp.Id

        LEFT JOIN dbo.TBCategoria ct
            ON ct.Id = dc.CategoriaId

        ORDER BY dp.DataDeOcorrencia DESC;
    """;

    public override void Cadastrar(Despesa despesa)
    {
        using SqlConnection conexao = AbrirConexao();

        conexao.Execute(
            InserirSql,
            CriarParametros(despesa)
        );


        foreach (Categoria categoria in despesa.Categorias)
        {
            conexao.Execute("""
            INSERT INTO dbo.TBDespesaCategoria
            (
                Id,
                DespesaId,
                CategoriaId
            )
            VALUES
            (
                @Id,
                @DespesaId,
                @CategoriaId
            );
        """,
            new
            {
                Id = Guid.NewGuid(),
                DespesaId = despesa.Id,
                CategoriaId = categoria.Id
            });
        }
    }

    public override bool Editar(Guid idSelecionado, Despesa despesa)
    {
        using SqlConnection conexao = AbrirConexao();

        conexao.Execute(
            AtualizarSql,
            CriarParametros(despesa)
        );


        conexao.Execute("""
        DELETE FROM dbo.TBDespesaCategoria
        WHERE DespesaId = @Id;
    """,
        new { Id = idSelecionado });


        foreach (Categoria categoria in despesa.Categorias)
        {
            conexao.Execute("""
            INSERT INTO dbo.TBDespesaCategoria
            (
                Id,
                DespesaId,
                CategoriaId
            )
            VALUES
            (
                @Id,
                @DespesaId,
                @CategoriaId
            );
        """,
            new
            {
                Id = Guid.NewGuid(),
                DespesaId = idSelecionado,
                CategoriaId = categoria.Id
            });
        }


        return true;
    }

    public override bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = AbrirConexao();

        conexao.Execute("""
        DELETE FROM dbo.TBDespesaCategoria
        WHERE DespesaId = @Id;
    """,
        new { Id = idSelecionado });


        return conexao.Execute(
            ExcluirSql,
            new { Id = idSelecionado }
        ) == 1;
    }

    public override Despesa? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = AbrirConexao();

        List<DespesaRow> rows = conexao
            .Query<DespesaRow>(
                SelecionarPorIdSql,
                new { Id = idSelecionado }
            )
            .ToList();


        if (!rows.Any())
            return null;


        return MapearDespesa(rows);
    }


    public override List<Despesa> SelecionarTodos()
    {
        using SqlConnection conexao = AbrirConexao();

        return conexao
            .Query<DespesaRow>(SelecionarTodosSql)
            .GroupBy(x => x.DespesaId)
            .Select(MapearDespesa)
            .ToList();
    }



    private static Despesa MapearDespesa(IEnumerable<DespesaRow> rows)
    {
        DespesaRow primeira = rows.First();


        return new Despesa
        {
            Id = primeira.DespesaId,

            Descricao = primeira.Descricao,

            DataDeOcorrencia = primeira.DataDeOcorrencia,

            Valor = primeira.Valor,

            FormaDePagamento = primeira.FormaDePagamento,


            Categorias = rows
                .Where(x => x.CategoriaId.HasValue)
                .Select(x => new Categoria
                {
                    Id = x.CategoriaId!.Value,
                    Titulo = x.CategoriaTitulo ?? ""
                })
                .ToList()
        };
    }



    protected override object CriarParametros(Despesa despesa)
    {
        return new
        {
            despesa.Id,
            despesa.Descricao,
            DataDeOcorrencia = despesa.DataDeOcorrencia,
            despesa.Valor,
            FormaDePagamento = (int)despesa.FormaDePagamento
        };
    }
}



public sealed class DespesaRow
{
    public Guid DespesaId { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public DateTime DataDeOcorrencia { get; set; }

    public decimal Valor { get; set; }

    public FormaDePagamento FormaDePagamento { get; set; }


    public Guid? CategoriaId { get; set; }

    public string? CategoriaTitulo { get; set; }
}