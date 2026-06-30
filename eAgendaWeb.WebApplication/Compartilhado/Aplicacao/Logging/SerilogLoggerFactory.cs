using System;
using Serilog;

namespace eAgendaWeb.WebApplication.Compartilhado.Aplicacao.Logging;

public static class SerilogLoggerFactory
{
    public static void AddSerilogLogger(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging
    )
    {
        Log.Logger = SerilogFactory.Create(configuration);

        // Remove o provedor padrão de logs da Microsoft
        logging.ClearProviders();

        services.AddSerilog(Log.Logger);
    }
}
