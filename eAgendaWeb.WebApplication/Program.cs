using eAgendaWeb.WebApplication.Compartilhado.Aplicacao;
using eAgendaWeb.WebApplication.Compartilhado.Apresentacao;
using eAgendaWeb.WebApplication.Compartilhado.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraRepositories();

builder.Services.AddApplicationServices(builder.Configuration, builder.Logging);

builder.Services.AddPresentationConfig(builder.Configuration);

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
