using System;
using eAgendaWeb.WebApplication.Compartilhado.Infra.Arquivo;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;

namespace eAgendaWeb.WebApplication.ModuloDeCategoria.Infra;

public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Categoria> CarregarRegistros()
    {
        return contexto.Categorias;
    }
}
