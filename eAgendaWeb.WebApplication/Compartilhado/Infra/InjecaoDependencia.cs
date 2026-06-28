using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.Compartilhado.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Infra;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using eAgendaWeb.WebApplication.ModuloDispesa.Infra;

namespace eAgendaWeb.WebApplication.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        services.AddScoped<IRepositorioDispesa, RepositorioDispesaEmArquivo>();


    }
}
