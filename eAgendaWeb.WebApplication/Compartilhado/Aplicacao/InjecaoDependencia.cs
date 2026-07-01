using System;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Aplicacao;
using eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;
using eAgendaWeb.WebApplication.ModuloItem.Aplicacao;
using eAgendaWeb.WebApplication.ModuloTarefa.Aplicacao;

namespace eAgendaWeb.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
       this IServiceCollection services,
       IConfiguration configuration,
       ILoggingBuilder logging
    )
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoDispesa>();
        services.AddScoped<ServicoTarefa>();
        services.AddScoped<ServicoItem>();
    }
}