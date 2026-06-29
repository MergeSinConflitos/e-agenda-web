using System;
using System.Text.Json.Serialization;
using eAgendaWeb.WebApplication.Compartilhado.Dominio;
using eAgendaWeb.WebApplication.ModuloTarefa.Dominio;

namespace eAgendaWeb.WebApplication.ModuloItem.Dominio;

public class Item : EntidadeBase<Item>
{
    public string Titulo { get; set; }

    public StatusDeConclusao StatusDeConclusao { get; set; }

    public Guid TarefaId { get; set; }


    [JsonIgnore]
    public Tarefa Tarefa { get; set; }


    public Item(
        string titulo,
        StatusDeConclusao statusDeConclusao,
        Tarefa tarefa)
    {
        Titulo = titulo;
        StatusDeConclusao = statusDeConclusao;
        Tarefa = tarefa;
        TarefaId = tarefa.Id;
    }

    public Item()
    {

    }

    public override void Atualizar(Item entidadeAtualizada)
    {
        Item itemAtualizado = (Item)entidadeAtualizada;

        Titulo = itemAtualizado.Titulo;
        StatusDeConclusao = itemAtualizado.StatusDeConclusao;
        Tarefa = itemAtualizado.Tarefa;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            erros.Add("O campo \"Titulo\"é obrigatorio");
        }
        else if (Titulo.Length > 100 || Titulo.Length < 2)
        {
            erros.Add("O titulo deve ter entre 2 e 100 caracteres");
        }

        if (Tarefa == null)
        {
            erros.Add("Selecione uma tarefa");
        }

        return erros;
    }
}