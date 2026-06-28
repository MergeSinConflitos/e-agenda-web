using System;
using System.ComponentModel.DataAnnotations;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Apresentacao;

public record OpcaoCategoriaViewModel(
    Guid Id,
    string Titulo
);

public record ListarDespesaViewModel(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<string> Categorias
);

public record CadastrarDespesaViewModel(

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "A descrição deve ter entre 2 e 100 caracteres")]
    string Descricao,

    [Required(ErrorMessage = "Informe a data da ocorrência")]
    DateTime? DataDeOcorrencia,

    [Range(0.01, double.MaxValue, ErrorMessage = "Informe um valor válido")]
    decimal Valor,

    [Required(ErrorMessage = "Selecione uma forma de pagamento")]
    FormaDePagamento FormaDePagamento,

    [Required(ErrorMessage = "Selecione pelo menos uma categoria")]
    List<Guid> CategoriaIds,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record EditarDespesaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "A descrição deve ter entre 2 e 100 caracteres")]
    string Descricao,

    [Required(ErrorMessage = "Informe a data da ocorrência")]
    DateTime? DataDeOcorrencia,

    [Range(0.01, double.MaxValue, ErrorMessage = "Informe um valor válido")]
    decimal Valor,

    [Required(ErrorMessage = "Selecione uma forma de pagamento")]
    FormaDePagamento FormaDePagamento,

    [Required(ErrorMessage = "Selecione pelo menos uma categoria")]
    List<Guid> CategoriaIds,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record ExcluirDespesaViewModel(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<string> Categorias
);
