using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloDispesa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Infra;

public class RepositorioDespesaEmArquivo : RepositorioBaseEmArquivo<Despesa>, IRepositorioDespesa
{
    public RepositorioDespesaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Despesa> CarregarRegistros()
    {
        return contexto.Dispesas;
    }
}
