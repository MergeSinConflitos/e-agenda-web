using System;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;
using FluentResults;

namespace eAgendaWeb.WebApplication.ModuloTarefa.Aplicacao;

public class ServicoTarefa
{
    private readonly IRepositorioTarefa repositorioTarefa;

    public ServicoTarefa(IRepositorioTarefa repositorioTarefa)
    {
        this.repositorioTarefa = repositorioTarefa;
    }

    public Result Cadastrar(CadastrarTarefaDto dto)
    {
        if (ExisteTarefaComMesmoTitulo(dto.Titulo))
        {
            return Falha("Titulo", "Já existe uma tarefa com esse Titulo");
        }

        Tarefa novaTarefa = new Tarefa(
            dto.Titulo,
            dto.Prioridade,
            dto.DataDeCriacao,
            dto.DataDeConclusao,
            dto.StatusDeConclusao
        );

        Result resultadoValidacao = ValidarEntidade(novaTarefa);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        repositorioTarefa.Cadastrar(novaTarefa);

        return Result.Ok().WithSuccess("Tarefa cadastrada com sucesso");
    }


    public Result Editar(EditarTarefaDto dto)
    {
        if (ExisteTarefaComMesmoTitulo(dto.Titulo, dto.Id))
        {
            return Falha("Titulo", "Já existe uma tarefa com esse Titulo");
        }

        Tarefa tarefaAtualizada = new Tarefa(
            dto.Titulo,
            dto.Prioridade,
            dto.DataDeCriacao,
            dto.DataDeConclusao,
            dto.StatusDeConclusao
        );

        Result resultadoValidacao = ValidarEntidade(tarefaAtualizada);

        if (resultadoValidacao.IsFailed)
        {
            return resultadoValidacao;
        }

        bool conseguiuEditar = repositorioTarefa.Editar(dto.Id, tarefaAtualizada);

        if (!conseguiuEditar)
            return Result.Fail("Tarefa não encontrada.");

        return Result.Ok().WithSuccess("Tarefa editada com sucesso");
    }


    public Result Excluir(Guid id)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(id);

        if (tarefa == null)
        {
            return Result.Fail("Tarefa não encontrada");
        }

        repositorioTarefa.Excluir(id);

        return Result.Ok().WithSuccess("Tarefa excluída com sucesso");
    }


    public List<ListarTarefaDto> SelecionarTodos()
    {
        return repositorioTarefa.SelecionarTodos()
            .Select(t => new ListarTarefaDto(
                t.Id,
                t.Titulo,
                t.Prioridade,
                t.DataDeCriacao,
                t.DataDeConclusao,
                t.StatusDeConclusao,
                t.PercentualConcluido
            )).ToList();
    }


    public Result<DetalhesTarefaDto> SelecionarPorId(Guid id)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(id);

        if (tarefa == null)
            return Result.Fail("Tarefa não encontrada.");

        return Result.Ok(new DetalhesTarefaDto(
            tarefa.Id,
            tarefa.Titulo,
            tarefa.Prioridade,
            tarefa.DataDeCriacao,
            tarefa.DataDeConclusao,
            tarefa.StatusDeConclusao,
            tarefa.PercentualConcluido
        ));
    }


    private bool ExisteTarefaComMesmoTitulo(string titulo, Guid? idIgnorado = null)
    {
        List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

        return tarefas.Any(t =>
            t.Id != idIgnorado &&
            string.Equals(t.Titulo, titulo, StringComparison.OrdinalIgnoreCase));
    }


    private static Result ValidarEntidade(Tarefa tarefa)
    {
        List<string> erros = tarefa.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(
            new Error(erros.First())
                .WithMetadata("Campo", string.Empty)
        );
    }


    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem)
            .WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}