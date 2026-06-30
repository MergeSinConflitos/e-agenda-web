using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloCompromisso.Dominio;

namespace eAgendaWeb.WebApplication.ModuloCompromisso.Infra;

public class RepositorioCompromissoEmArquivo : RepositorioBaseEmArquivo<Compromisso>, IRepositorioCompromisso
{
    public RepositorioCompromissoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Compromisso> CarregarRegistros()
    {
        return contexto.Compromisso;
    }
}
