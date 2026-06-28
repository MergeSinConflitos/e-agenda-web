using System;
using AutoMapper;
using eAgendaWeb.WebApplication.Compartilhado.Apresentacao.Extensions;
using eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Apresentacao;

public class DispesaController : Controller
{
    private readonly ServicoDispesa servicoDispesa;
    private readonly IMapper mapeador;

    public DispesaController(ServicoDispesa servicoDispesa, IMapper mapeador)
    {
        this.servicoDispesa = servicoDispesa;
        this.mapeador = mapeador;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarDispesaDto> dtos = servicoDispesa.SelecionarTodos();
        List<ListarDispesaViewModel> listarVms = mapeador.Map<List<ListarDispesaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarDispesaViewModel cadastrarVm = new(
            string.Empty,
            DateTime.Now,
            0,
            FormaDePagamento.AVista,
            new List<Guid>(),
            SelecionarCategorias()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarDispesaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Categorias = SelecionarCategorias() });

        CadastrarDispesaDto dto = mapeador.Map<CadastrarDispesaDto>(cadastrarVm);

        Result resultado = servicoDispesa.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Categorias = SelecionarCategorias() });
        }

        TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesDispesaDto> resultado = servicoDispesa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarDispesaViewModel editarVm =
            mapeador.Map<EditarDispesaViewModel>(resultado.Value)
            with
            {
                Categorias = SelecionarCategorias()
            };

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarDispesaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with { Categorias = SelecionarCategorias() });

        EditarDispesaDto dto = mapeador.Map<EditarDispesaDto>(editarVm);

        Result resultado = servicoDispesa.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm with { Categorias = SelecionarCategorias() });
        }

        TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesDispesaDto> resultado = servicoDispesa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirDispesaViewModel excluirVm =
            mapeador.Map<ExcluirDispesaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirDispesaViewModel excluirVm)
    {
        Result resultado = servicoDispesa.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        TempData.AddSuccessMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoCategoriaViewModel> SelecionarCategorias()
    {
        List<OpcaoCategoriaDto> dtos = servicoDispesa.SelecionarCategorias();

        return mapeador.Map<List<OpcaoCategoriaViewModel>>(dtos);
    }
}
