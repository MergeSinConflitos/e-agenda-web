using System;
using eAgendaWeb.WebApplication.Compartilhado.Dominio;
using eAgendaWeb.WebApplication.ModuloDeCategoria.Dominio;

namespace eAgendaWeb.WebApplication.ModuloDispesa.Dominio;

public enum FormaDePagamento
{
    AVista,
    Credito,
    Debito
}
public class Dispesa : EntidadeBase<Dispesa>
{
    public string Descricao { get; set; }
    public DateTime? DataDeOcorrencia { get; set; } = DateTime.Now;
    public decimal Valor { get; set; }
    public FormaDePagamento FormaDePagamento { get; set; }
    public List<Categoria> Categorias { get; set; }
    public Dispesa(string descricao, DateTime? dataDeOcorrencia, decimal valor, FormaDePagamento formaDePagamento, List<Categoria> categorias)
    {
        Descricao = descricao;
        DataDeOcorrencia = dataDeOcorrencia;
        Valor = valor;
        FormaDePagamento = formaDePagamento;
        Categorias = categorias;
    }

    public Dispesa()
    {

    }

    public override void Atualizar(Dispesa entidadeAtualizada)
    {
        Dispesa dispesaAtualizada = (Dispesa)entidadeAtualizada;

        Descricao = dispesaAtualizada.Descricao;
        DataDeOcorrencia = dispesaAtualizada.DataDeOcorrencia;
        Valor = dispesaAtualizada.Valor;
        FormaDePagamento = dispesaAtualizada.FormaDePagamento;
        Categorias = dispesaAtualizada.Categorias;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Descricao))
        {
            erros.Add("O campo \"Descrição\"deve ser preenchido");
        }
        else if (Descricao.Length > 100 || Descricao.Length < 2)
        {
            erros.Add("A descrição deve ter entre 2 e 100 caracteres");
        }

        if (DataDeOcorrencia > DateTime.Now)
        {
            erros.Add("Data inválida");
        }

        if (Valor < 0)
        {
            erros.Add("Valor inválido");
        }

        if (Categorias == null)
        {
            erros.Add("Categoria é obrigatório");
        }

        return erros;
    }
}
