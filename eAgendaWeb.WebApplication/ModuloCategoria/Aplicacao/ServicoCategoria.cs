using System;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using FluentResults;

namespace eAgendaWeb.WebApplication.ModuloDeCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IRepositorioDespesa repositorioDespesa;

    public ServicoCategoria(IRepositorioCategoria repositorioCategoria, IRepositorioDespesa repositorioDespesa)
    {
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioDespesa = repositorioDespesa;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        if (ExisteCategoriaComMesmoTitulo(dto.Titulo))
        {
            return Falha("Titulo", "Já existe uma categoria com esse Titulo");
        }

        Categoria novaCategoria = new Categoria(
            dto.Titulo
        );

        Result resultadoValidacao = ValidarEntidade(novaCategoria);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        repositorioCategoria.Cadastrar(novaCategoria);
        return Result.Ok().WithSuccess("Categoria cadastrado com sucesso");
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        if (ExisteCategoriaComMesmoTitulo(dto.Titulo, dto.Id))
        {
            return Falha("Titulo", "Já existe uma categoria com esse Titulo");
        }

        Categoria categoriaAtualizada = new Categoria(
            dto.Titulo
        );

        Result resultadoValidacao = ValidarEntidade(categoriaAtualizada);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        bool conseguiuEditar = repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Categoria não encontrada.");


        return Result.Ok().WithSuccess("Categoria editada com sucesso");
    }

    public Result Excluir(Guid Id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(Id);

        if (categoria == null)
        {
            return Result.Fail("Categoria não encontrada");
        }

        if (ExisteDespesaNaCategoria(Id))
        {
            return Result.Fail("Essa categoria não pode ser excluida pois possui despesas vinculadas a ela");
        }

        repositorioCategoria.Excluir(Id);

        return Result.Ok().WithSuccess("Categoria excluida com sucesso");
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        return repositorioCategoria.SelecionarTodos()
        .Select(c => new ListarCategoriasDto(
            c.Id,
            c.Titulo)).ToList();
    }

    public Result<DetalhesCategoriaDto> SelecionarPorId(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrado.");

        return Result.Ok(new DetalhesCategoriaDto(
            categoria.Id,
            categoria.Titulo));
    }

    private bool ExisteCategoriaComMesmoTitulo(string titulo, Guid? idIgnorado = null)
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        return categorias.Any(f => f.Id != idIgnorado && string.Equals(f.Titulo, titulo, StringComparison.OrdinalIgnoreCase));
    }

    private static Result ValidarEntidade(Categoria categoria)
    {
        List<string> erros = categoria.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }


    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
    private bool ExisteDespesaNaCategoria(Guid categoriaId)
    {
        return repositorioDespesa
            .SelecionarTodos()
            .Any(d => d.Categorias.Any(c => c.Id == categoriaId));
    }
}


