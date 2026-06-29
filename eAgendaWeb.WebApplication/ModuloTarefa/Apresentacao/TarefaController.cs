using AutoMapper;
using eAgendaWeb.WebApplication.Compartilhado.Apresentacao.Extensions;
using eAgendaWeb.WebApplication.ModuloTarefa.Aplicacao;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaWeb.WebApplication.ModuloTarefa.Apresentacao;

public class TarefaController : Controller
{
    private readonly ServicoTarefa servicoTarefa;
    private readonly IMapper mapeador;

    public TarefaController(ServicoTarefa servicoTarefa, IMapper mapeador)
    {
        this.servicoTarefa = servicoTarefa;
        this.mapeador = mapeador;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarTarefaDto> dtos = servicoTarefa.SelecionarTodos();

        List<ListarTarefaViewModel> listarVms =
            mapeador.Map<List<ListarTarefaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTarefaViewModel cadastrarVm = new(
            string.Empty,
            Prioridade.Normal,
            DateTime.Now
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTarefaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarTarefaDto dto = mapeador.Map<CadastrarTarefaDto>(cadastrarVm);

        Result resultado = servicoTarefa.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(cadastrarVm);
        }

        TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesTarefaDto> resultado = servicoTarefa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        EditarTarefaViewModel editarVm =
            mapeador.Map<EditarTarefaViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarTarefaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarTarefaDto dto = mapeador.Map<EditarTarefaDto>(editarVm);

        Result resultado = servicoTarefa.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(editarVm);
        }

        TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesTarefaDto> resultado = servicoTarefa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirTarefaViewModel excluirVm =
            mapeador.Map<ExcluirTarefaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTarefaViewModel excluirVm)
    {
        Result resultado = servicoTarefa.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);
        else
            TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}