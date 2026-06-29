using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloTarefa.Infra;

public class RepositorioTarefaEmArquivo : RepositorioBaseEmArquivo<Tarefa>, IRepositorioTarefa
{
    public RepositorioTarefaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Tarefa> CarregarRegistros()
    {
        return contexto.Tarefas;
    }
}
