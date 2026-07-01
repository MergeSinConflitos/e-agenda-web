using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.Compartilhado.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloCategoria.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Infra;
using eAgendaWeb.WebApplication.ModuloDespesa.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;
using eAgendaWeb.WebApplication.ModuloDispesa.Infra;
using eAgendaWeb.WebApplication.ModuloItem.Dominio;
using eAgendaWeb.WebApplication.ModuloItem.Infra;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;
using eAgendaWeb.WebApplication.ModuloTarefa.Infra;
using eAgendaWeb.WebApplication.ModuloTarefa.Infra.SQL;

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

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmSql>();
        services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmSql>();
        services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmSql>();
        services.AddScoped<IRepositorioItem, RepositorioItemEmArquivo>();


    }
}
