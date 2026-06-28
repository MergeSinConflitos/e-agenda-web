using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Infra;

public class RepositorioDispesaEmArquivo : RepositorioBaseEmArquivo<Dispesa>, IRepositorioDispesa
{
    public RepositorioDispesaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Dispesa> CarregarRegistros()
    {
        return contexto.Dispesas;
    }
}
