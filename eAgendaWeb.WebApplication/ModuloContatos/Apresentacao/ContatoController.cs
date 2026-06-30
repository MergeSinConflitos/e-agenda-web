using AutoMapper;
using eAgendaWeb.WebApplication.Compartilhado.Apresentacao.Extensions;
using eAgendaWeb.WebApplication.ModuloContatos.Aplicacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;


namespace eAgendaWeb.WebApplication.ModuloContatos.Apresentacao;

public class ContatoController : Controller
{
    IMapper mapeador;

    ServicoContato servicoContato;

    public ContatoController(IMapper mapeador, ServicoContato servicoContato)
    {
        this.mapeador = mapeador;
        this.servicoContato = servicoContato;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarContatosDto> dtos = servicoContato.SelecionarTodos();

        List<ListarContatosViewModel> listarVms = mapeador.Map<List<ListarContatosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarContatosViewModel cadastrarVm = new CadastrarContatosViewModel(
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarContatosViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarContatoDto dto = mapeador.Map<CadastrarContatoDto>(cadastrarVm);

        Result resultado = servicoContato.Cadastrar(dto);

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
        Result<DetalhesContatoDto> resultado = servicoContato.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesContatoDto dto = resultado.Value;

        EditarContatoViewModel editarVm = mapeador.Map<EditarContatoViewModel>(dto);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarContatoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarContatoDto dto = mapeador.Map<EditarContatoDto>(editarVm);

        Result resultado = servicoContato.Editar(dto);

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
        Result<DetalhesContatoDto> resultado = servicoContato.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesContatoDto dto = resultado.Value;

        ExcluirContatoViewModel excluirVm = mapeador.Map<ExcluirContatoViewModel>(dto);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirContatoViewModel excluirVm)
    {
        Result resultado = servicoContato.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);
        else
        {
            TempData.AddSuccessMessage(resultado);
        }
        return RedirectToAction(nameof(Listar));
    }

}
