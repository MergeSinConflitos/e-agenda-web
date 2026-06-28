using System;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;

public record OpcaoCategoriaDto(
    Guid Id,
    string Titulo
);

public record ListarDispesaDto(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<string> Categorias
);

public record CadastrarDispesaDto(
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<Guid> CategoriaIds
);

public record EditarDispesaDto(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<Guid> CategoriaIds
);

public record DetalhesDispesaDto(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<string> Categorias
);
