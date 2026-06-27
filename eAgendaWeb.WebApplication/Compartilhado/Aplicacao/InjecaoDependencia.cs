using System;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Aplicacao;

namespace eAgendaWeb.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
    }
}