using System;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using FluentResults;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;

public class ServicoDispesa
{
    private readonly IRepositorioDispesa repositorioDispesa;
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoDispesa(
        IRepositorioDispesa repositorioDispesa,
        IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioDispesa = repositorioDispesa;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarDispesaDto dto)
    {
        List<Categoria> categorias = dto.CategoriaIds
            .Select(id => repositorioCategoria.SelecionarPorId(id))
            .Where(c => c != null)
            .Cast<Categoria>()
            .ToList();

        if (categorias.Count != dto.CategoriaIds.Count)
            return Falha(nameof(dto.CategoriaIds), "Selecione categorias válidas.");

        Dispesa novaDispesa = new(
            dto.Descricao,
            dto.DataDeOcorrencia,
            dto.Valor,
            dto.FormaDePagamento,
            categorias
        );

        Result resultadoValidacao = ValidarEntidade(novaDispesa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioDispesa.Cadastrar(novaDispesa);

        return Result.Ok().WithSuccess("Despesa cadastrada com sucesso.");
    }

    public Result Editar(EditarDispesaDto dto)
    {
        List<Categoria> categorias = dto.CategoriaIds
            .Select(id => repositorioCategoria.SelecionarPorId(id))
            .Where(c => c != null)
            .Cast<Categoria>()
            .ToList();

        if (categorias.Count != dto.CategoriaIds.Count)
            return Falha(nameof(dto.CategoriaIds), "Selecione categorias válidas.");

        Dispesa dispesaAtualizada = new(
            dto.Descricao,
            dto.DataDeOcorrencia,
            dto.Valor,
            dto.FormaDePagamento,
            categorias
        );

        Result resultadoValidacao = ValidarEntidade(dispesaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioDispesa.Editar(dto.Id, dispesaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Despesa não encontrada.");

        return Result.Ok().WithSuccess("Despesa editada com sucesso.");
    }

    public Result Excluir(Guid id)
    {
        Dispesa? dispesa = repositorioDispesa.SelecionarPorId(id);

        if (dispesa == null)
        {
            return Result.Fail("Despesa não encontrada");
        }

        repositorioDispesa.Excluir(id);

        return Result.Ok().WithSuccess("Despesa excluída com sucesso");
    }

    public List<ListarDispesaDto> SelecionarTodos()
    {
        return repositorioDispesa.SelecionarTodos()
            .Select(d => new ListarDispesaDto(
                d.Id,
                d.Descricao,
                d.DataDeOcorrencia,
                d.Valor,
                d.FormaDePagamento,
                d.Categorias.Select(c => c.Titulo).ToList()
            ))
            .ToList();
    }

    public Result<DetalhesDispesaDto> SelecionarPorId(Guid id)
    {
        Dispesa? dispesa = repositorioDispesa.SelecionarPorId(id);

        if (dispesa == null)
            return Result.Fail("Despesa não encontrada.");

        return Result.Ok(new DetalhesDispesaDto(
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

    private static Result ValidarEntidade(Dispesa dispesa)
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
