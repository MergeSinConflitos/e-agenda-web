using System;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;

public record OpcaoCategoriaDto(
    Guid Id,
    string Titulo
);

public record ListarDespesaDto(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<string> Categorias
);

public record CadastrarDespesaDto(
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<Guid> CategoriaIds
);

public record EditarDespesaDto(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<Guid> CategoriaIds
);

public record DetalhesDespesaDto(
    Guid Id,
    string Descricao,
    DateTime? DataDeOcorrencia,
    decimal Valor,
    FormaDePagamento FormaDePagamento,
    List<Guid> CategoriaIds,
    List<string> Categorias
);