
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.Compartilhado.Infra.SQL;
using eAgendaWeb.WebApplication.ModuloCompromisso.Dominio;
using eAgendaWeb.WebApplication.ModuloCompromisso.Infra;
using eAgendaWeb.WebApplication.ModuloContato.Dominio;
using eAgendaWeb.WebApplication.ModuloContato.Infra;

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

        //services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IRepositorioContato, RepositorioContatoEmArquivo>();
        services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmArquivo>();

    }
}
