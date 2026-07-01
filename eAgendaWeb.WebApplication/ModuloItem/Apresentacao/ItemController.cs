using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentResults;
using eAgendaWeb.WebApplication.ModuloItem.Aplicacao;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;
using eAgendaWeb.WebApplication.Compartilhado.Apresentacao.Extensions;

public class ItemController : Controller
{
    private readonly ServicoItem servicoItem;
    private readonly IMapper mapeador;

    public ItemController(
        ServicoItem servicoItem,
        IMapper mapeador)
    {
        this.servicoItem = servicoItem;
        this.mapeador = mapeador;
    }


    public ActionResult Listar(Guid tarefaId)
    {
        List<ListarItemDto> dtos =
            servicoItem.SelecionarTodosPorTarefa(tarefaId);


        GerenciarItemViewModel gerenciarVm =
            new GerenciarItemViewModel(
                tarefaId,
                dtos.FirstOrDefault()?.TarefaTitulo ?? string.Empty,
                mapeador.Map<List<ListarItemViewModel>>(dtos)
            );


        return View(gerenciarVm);
    }



    [HttpGet]
    public ActionResult Cadastrar(Guid tarefaId)
    {
        CadastrarItemViewModel cadastrarVm =
            new CadastrarItemViewModel(
                string.Empty,
                tarefaId,
                StatusDeConclusao.Aberto,
                SelecionarTarefas()
            );


        return View(cadastrarVm);
    }



    [HttpPost]
    public ActionResult Cadastrar(CadastrarItemViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with
            {
                Tarefas = SelecionarTarefas()
            });


        CadastrarItemDto dto =
            mapeador.Map<CadastrarItemDto>(cadastrarVm);


        Result resultado =
            servicoItem.Cadastrar(dto);


        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with
            {
                Tarefas = SelecionarTarefas()
            });
        }


        return RedirectToAction(
            nameof(Listar),
            new { tarefaId = cadastrarVm.TarefaId }
        );
    }

    [HttpPost]
    public ActionResult Concluir(Guid id, Guid tarefaId)
    {
        Result resultado = servicoItem.Concluir(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;
        }
        else
        {
            TempData["MensagemSucesso"] = "Item concluído!";
        }

        return RedirectToAction(
            nameof(Listar),
            new { tarefaId }
        );
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesItemDto> resultado =
            servicoItem.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Listar));

        return View(
            mapeador.Map<ExcluirItemViewModel>(resultado.Value)
        );
    }


    [HttpPost]
    public ActionResult Excluir(ExcluirItemViewModel excluirvm)
    {
        Result resultado = servicoItem.Excluir(excluirvm.Id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;
        }
        else
        {
            TempData["MensagemSucesso"] = "Item excluído com sucesso.";
        }

        return RedirectToAction(
            nameof(Listar),
            new { tarefaId = excluirvm.TarefaId }
        );
    }

    private List<OpcaoTarefaViewModel> SelecionarTarefas()
    {
        List<OpcaoTarefaDto> dtos =
            servicoItem.SelecionarTarefas();


        return mapeador.Map<List<OpcaoTarefaViewModel>>(dtos);
    }
}