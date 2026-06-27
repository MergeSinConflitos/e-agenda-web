using System;
using eAgendaWeb.WebApplication.Compartilhado.Dominio;

namespace eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;

public class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; }

    public Categoria(string titulo)
    {
        Titulo = titulo;
    }

    public Categoria()
    {

    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            erros.Add("O campo \"Titulo\"deve ser preenchido");
        }
        else if (Titulo.Length > 100 || Titulo.Length < 2)
        {
            erros.Add("O titulo deve ter entre 2 e 100 caracteres");
        }

        return erros;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Categoria categoriaAtualizada = (Categoria)entidadeAtualizada;

        Titulo = categoriaAtualizada.Titulo;
    }
}
