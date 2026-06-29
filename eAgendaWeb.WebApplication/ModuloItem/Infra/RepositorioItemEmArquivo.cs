using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloItem.Dominio;

namespace eAgendaWeb.WebApplication.ModuloItem.Infra;

public class RepositorioItemEmArquivo : RepositorioBaseEmArquivo<Item>, IRepositorioItem
{
    public RepositorioItemEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Item> CarregarRegistros()
    {
        return contexto.Items;
    }
}
