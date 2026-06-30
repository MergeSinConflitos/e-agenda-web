using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloContato.Dominio;

namespace eAgendaWeb.WebApplication.ModuloContato.Infra;

public class RepositorioContatoEmArquivo : RepositorioBaseEmArquivo<Contato>, IRepositorioContato
{
    public RepositorioContatoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Contato> CarregarRegistros()
    {
        return contexto.Contato;
    }
}
