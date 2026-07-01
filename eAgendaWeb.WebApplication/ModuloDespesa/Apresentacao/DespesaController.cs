using System;
using AutoMapper;
using eAgendaWeb.WebApplication.Compartilhado.Apresentacao.Extensions;
using eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Apresentacao;

public class DespesaController : Controller
{
    private readonly ServicoDispesa servicoDispesa;
    private readonly IMapper mapeador;

    public DespesaController(ServicoDispesa servicoDispesa, IMapper mapeador)
    {
        this.servicoDispesa = servicoDispesa;
        this.mapeador = mapeador;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarDespesaDto> dtos = servicoDispesa.SelecionarTodos();
        List<ListarDespesaViewModel> listarVms = mapeador.Map<List<ListarDespesaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarDespesaViewModel cadastrarVm = new(
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
    public ActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Categorias = SelecionarCategorias() });

        CadastrarDespesaDto dto = mapeador.Map<CadastrarDespesaDto>(cadastrarVm);

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
        Result<DetalhesDespesaDto> resultado = servicoDispesa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarDespesaViewModel editarVm =
            mapeador.Map<EditarDespesaViewModel>(resultado.Value)
            with
            {
                Categorias = SelecionarCategorias()
            };

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarDespesaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with { Categorias = SelecionarCategorias() });

        EditarDespesaDto dto = mapeador.Map<EditarDespesaDto>(editarVm);

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
        Result<DetalhesDespesaDto> resultado = servicoDispesa.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirDespesaViewModel excluirVm =
            mapeador.Map<ExcluirDespesaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirDespesaViewModel excluirVm)
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
