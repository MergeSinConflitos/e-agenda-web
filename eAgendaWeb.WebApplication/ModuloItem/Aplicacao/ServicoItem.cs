using System;

namespace eAgendaWeb.WebApplication.ModuloItem.Aplicacao;

using System;
using eAgendaWeb.WebApplication.ModuloItem.Dominio;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;
using FluentResults;


public class ServicoItem
{
    private readonly IRepositorioItem repositorioItem;
    private readonly IRepositorioTarefa repositorioTarefa;

    public ServicoItem(
        IRepositorioItem repositorioItem,
        IRepositorioTarefa repositorioTarefa)
    {
        this.repositorioItem = repositorioItem;
        this.repositorioTarefa = repositorioTarefa;
    }


    public Result Cadastrar(CadastrarItemDto dto)
    {
        Result<Tarefa> resultadoTarefa = SelecionarTarefa(dto.TarefaId);

        if (resultadoTarefa.IsFailed)
            return Result.Fail(resultadoTarefa.Errors);


        Item novoItem = new Item(
     dto.Titulo,
     dto.StatusDeConclusao,
     resultadoTarefa.Value
 );

        novoItem.TarefaId = resultadoTarefa.Value.Id;


        Result resultadoValidacao = ValidarEntidade(novoItem);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;


        repositorioItem.Cadastrar(novoItem);

        return Result.Ok()
            .WithSuccess("Item cadastrado com sucesso");
    }


    public List<ListarItemDto> SelecionarTodosPorTarefa(Guid tarefaId)
    {
        return repositorioItem
           .Filtrar(i => i.TarefaId == tarefaId)
            .Select(MapearParaListarDto)
            .ToList();
    }


    public Result<DetalhesItemDto> SelecionarPorId(Guid id)
    {
        Item? item = repositorioItem.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item não encontrado.");


        return Result.Ok(MapearParaDetalhesDto(item));
    }


    public List<OpcaoTarefaDto> SelecionarTarefas()
    {
        return repositorioTarefa
            .SelecionarTodos()
            .Select(t => new OpcaoTarefaDto(
                t.Id,
                t.Titulo
            ))
            .ToList();
    }

    public Result Concluir(Guid id)
    {
        Item? item = repositorioItem.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item não encontrado.");

        item.StatusDeConclusao = StatusDeConclusao.Concluido;

        repositorioItem.Editar(id, item);


        AtualizarPercentualDaTarefa(item.TarefaId);


        return Result.Ok()
            .WithSuccess("Item concluído com sucesso.");
    }

    public Result Excluir(Guid id)
    {
        Item? item = repositorioItem.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item não encontrado.");


        Guid tarefaId = item.TarefaId;


        repositorioItem.Excluir(id);


        AtualizarPercentualDaTarefa(tarefaId);


        return Result.Ok()
            .WithSuccess("Item excluído com sucesso.");
    }


    private Result<Tarefa> SelecionarTarefa(Guid tarefaId)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(tarefaId);

        if (tarefa == null)
            return Result.Fail(
                new Error("Selecione uma tarefa válida.")
                .WithMetadata("Campo", nameof(tarefaId))
            );


        return Result.Ok(tarefa);
    }



    private static Result ValidarEntidade(Item item)
    {
        List<string> erros = item.Validar();

        if (erros.Count == 0)
            return Result.Ok();


        return Result.Fail(
            new Error(erros.First())
            .WithMetadata("Campo", string.Empty)
        );
    }



    private static ListarItemDto MapearParaListarDto(Item item)
    {
        return new ListarItemDto(
            item.Id,
            item.Titulo,
            item.StatusDeConclusao,
            item.TarefaId,
            item.Tarefa?.Titulo ?? string.Empty
        );
    }


    private static DetalhesItemDto MapearParaDetalhesDto(Item item)
    {
        return new DetalhesItemDto(
            item.Id,
            item.Titulo,
            item.StatusDeConclusao,
            item.TarefaId,
            item.Tarefa?.Titulo ?? string.Empty
        );
    }
    private void AtualizarPercentualDaTarefa(Guid tarefaId)
    {
        Tarefa? tarefa = repositorioTarefa.SelecionarPorId(tarefaId);

        if (tarefa == null)
            return;


        List<Item> itens = repositorioItem
            .Filtrar(i => i.TarefaId == tarefaId)
            .ToList();


        if (!itens.Any())
        {
            tarefa.PercentualConcluido = 0;
            tarefa.StatusDeConclusao = StatusDeConclusao.Aberto;
            tarefa.DataDeConclusao = DateTime.MinValue;
        }
        else
        {
            int concluidos = itens.Count(i =>
                i.StatusDeConclusao == StatusDeConclusao.Concluido);


            tarefa.PercentualConcluido =
                (concluidos * 100) / itens.Count;


            if (concluidos == itens.Count)
            {
                tarefa.StatusDeConclusao = StatusDeConclusao.Concluido;
                tarefa.DataDeConclusao = DateTime.Now;
            }
            else
            {
                tarefa.StatusDeConclusao = StatusDeConclusao.Aberto;
                tarefa.DataDeConclusao = DateTime.MinValue;
            }
        }


        repositorioTarefa.Editar(tarefaId, tarefa);
    }
}