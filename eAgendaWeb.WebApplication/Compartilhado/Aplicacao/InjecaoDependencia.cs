using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloContato.Aplicacao;
using eAgendaWeb.WebApplication.ModuloContato.Dominio;
using eAgendaWeb.WebApplication.ModuloContato.Infra;

namespace eAgendaWeb.WebApplication.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoContato>();
    }
}