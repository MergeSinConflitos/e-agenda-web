
using eAgendaWeb.WebApplication.Compartilhado.Dominio;
using eAgendaWeb.WebApplication.ModuloContato.Dominio;

namespace eAgendaWeb.WebApplication.ModuloCompromisso.Dominio;

public class Compromisso : EntidadeBase<Compromisso>
{   
    public string Assunto { get; set; } = string.Empty;
    public DateTime DataOcorrencia { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraTermino { get; set; }
    public TipoCompromisso Tipo { get; set; }
    public string? Local { get; set; }
    public string? Link { get; set; }
    public Guid? ContatoId { get; set; }
    public Contato? Contato { get; set; }

    public Compromisso(
        string assunto,
        DateTime dataOcorrencia,
        TimeSpan horaInicio,
        TimeSpan horaTermino,
        TipoCompromisso tipo,
        string? local,
        string? link,
        Contato? contato)
    {
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
        Tipo = tipo;
        Local = local;
        Link = link;
        Contato = contato;
        ContatoId = contato?.Id;
    }
    public Compromisso() { }

    public override void Atualizar(Compromisso entidadeAtualizada)
    {
        Assunto = entidadeAtualizada.Assunto;
        DataOcorrencia = entidadeAtualizada.DataOcorrencia;
        HoraInicio = entidadeAtualizada.HoraInicio;
        HoraTermino = entidadeAtualizada.HoraTermino;
        Tipo = entidadeAtualizada.Tipo;
        Local = entidadeAtualizada.Local;
        Link = entidadeAtualizada.Link;
        Contato = entidadeAtualizada.Contato;
        ContatoId = entidadeAtualizada.ContatoId;
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}
    public enum TipoCompromisso
{
    Presencial,
    Remoto
}

