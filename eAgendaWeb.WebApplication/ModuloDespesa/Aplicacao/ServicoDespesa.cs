using System;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using FluentResults;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;

public class ServicoDispesa
{
    private readonly IRepositorioDespesa repositorioDespesa;
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoDispesa(
        IRepositorioDespesa repositorioDispesa,
        IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioDespesa = repositorioDispesa;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarDespesaDto dto)
    {
        List<Categoria> categorias = dto.CategoriaIds
            .Select(id => repositorioCategoria.SelecionarPorId(id))
            .Where(c => c != null)
            .Cast<Categoria>()
            .ToList();

        if (categorias.Count != dto.CategoriaIds.Count)
            return Falha(nameof(dto.CategoriaIds), "Selecione categorias válidas.");

        Despesa novaDespesa = new(
            dto.Descricao,
            dto.DataDeOcorrencia,
            dto.Valor,
            dto.FormaDePagamento,
            categorias
        );

        Result resultadoValidacao = ValidarEntidade(novaDespesa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioDespesa.Cadastrar(novaDespesa);

        return Result.Ok().WithSuccess("Despesa cadastrada com sucesso.");
    }

    public Result Editar(EditarDespesaDto dto)
    {
        List<Categoria> categorias = dto.CategoriaIds
            .Select(id => repositorioCategoria.SelecionarPorId(id))
            .Where(c => c != null)
            .Cast<Categoria>()
            .ToList();

        if (categorias.Count != dto.CategoriaIds.Count)
            return Falha(nameof(dto.CategoriaIds), "Selecione categorias válidas.");

        Despesa despesaAtualizada = new(
            dto.Descricao,
            dto.DataDeOcorrencia,
            dto.Valor,
            dto.FormaDePagamento,
            categorias
        );

        Result resultadoValidacao = ValidarEntidade(despesaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioDespesa.Editar(dto.Id, despesaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Despesa não encontrada.");

        return Result.Ok().WithSuccess("Despesa editada com sucesso.");
    }

    public Result Excluir(Guid id)
    {
        Despesa? despesa = repositorioDespesa.SelecionarPorId(id);

        if (despesa == null)
        {
            return Result.Fail("Despesa não encontrada");
        }

        repositorioDespesa.Excluir(id);

        return Result.Ok().WithSuccess("Despesa excluída com sucesso");
    }

    public List<ListarDespesaDto> SelecionarTodos()
    {
        return repositorioDespesa.SelecionarTodos()
            .Select(d => new ListarDespesaDto(
                d.Id,
                d.Descricao,
                d.DataDeOcorrencia,
                d.Valor,
                d.FormaDePagamento,
                d.Categorias.Select(c => c.Titulo).ToList()
            ))
            .ToList();
    }

    public Result<DetalhesDespesaDto> SelecionarPorId(Guid id)
    {
        Despesa? dispesa = repositorioDespesa.SelecionarPorId(id);

        if (dispesa == null)
            return Result.Fail("Despesa não encontrada.");

        return Result.Ok(new DetalhesDespesaDto(
     dispesa.Id,
     dispesa.Descricao,
     dispesa.DataDeOcorrencia,
     dispesa.Valor,
     dispesa.FormaDePagamento,
     dispesa.Categorias.Select(c => c.Id).ToList(),
     dispesa.Categorias.Select(c => c.Titulo).ToList()
 ));
    }

    public List<OpcaoCategoriaDto> SelecionarCategorias()
    {
        return repositorioCategoria.SelecionarTodos()
            .Select(c => new OpcaoCategoriaDto(
                c.Id,
                c.Titulo
            ))
            .ToList();
    }

    private static Result ValidarEntidade(Despesa dispesa)
    {
        List<string> erros = dispesa.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(
            new Error(erros.First())
                .WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem)
            .WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
