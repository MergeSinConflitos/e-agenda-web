using System;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Aplicacao;
using eAgendaWeb.WebApplication.ModuloDispesa.Aplicacao;

namespace eAgendaWeb.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoDispesa>();
    }
}