using System;
using eAgendaWeb.WebApplication.Compartilhado.Dominio;

namespace eAgendaWeb.WebApplication.ModuloTarefa.Dominio;

public enum Prioridade
{
    Baixa, Normal, Alta
}

public enum StatusDeConclusao
{
    Aberto, Concluido
}
public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; }
    public Prioridade Prioridade { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime? DataDeConclusao { get; set; }
    public StatusDeConclusao StatusDeConclusao { get; set; }
    public double PercentualConcluido { get; set; } = 0;
    public Tarefa(string titulo, Prioridade prioridade, DateTime dataDeCriacao, DateTime? dataDeConclusao, StatusDeConclusao statusDeConclusao)
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataDeCriacao = dataDeCriacao;
        DataDeConclusao = dataDeConclusao;
        StatusDeConclusao = statusDeConclusao;
    }

    public Tarefa()
    {

    }
    public override void Atualizar(Tarefa entidadeAtualizada)
    {
        Tarefa tarefaAtualizada = (Tarefa)entidadeAtualizada;

        Titulo = tarefaAtualizada.Titulo;
        Prioridade = tarefaAtualizada.Prioridade;
        DataDeCriacao = tarefaAtualizada.DataDeCriacao;
        DataDeConclusao = tarefaAtualizada.DataDeConclusao;
        StatusDeConclusao = tarefaAtualizada.StatusDeConclusao;
        PercentualConcluido = tarefaAtualizada.PercentualConcluido;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            erros.Add("O campo \"Titulo\"é obrigatório");
        }
        else if (Titulo.Length > 100 || Titulo.Length < 2)
        {
            erros.Add("O titulo deve ter entre 2 e 100 caracteres");
        }

        if (DataDeCriacao > DateTime.Now)
        {
            erros.Add("Data inválida");
        }

        if (DataDeConclusao > DateTime.Now)
        {
            erros.Add("Data inválida");
        }

        if (PercentualConcluido < 0)
        {
            erros.Add("Valor inválido");
        }

        return erros;
    }


}
