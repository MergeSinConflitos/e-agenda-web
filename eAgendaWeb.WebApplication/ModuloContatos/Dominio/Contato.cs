
using eAgendaWeb.WebApplication.Compartilhado.Dominio;

namespace eAgendaWeb.WebApplication.ModuloContatos.Dominio;

public class Contato : EntidadeBase<Contato>
{   
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public string? Empresa { get; set; }

    public Contato(string nome, string email, string telefone, string? cargo, string? empresa)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }
    
    public Contato() { }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" é obrigatório.");
        else if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Email))
            erros.Add("O campo \"Email\" é obrigatório.");
        else if (!Email.Contains('@') || !Email.Contains('.'))
            erros.Add("Informe um e-mail válido.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" é obrigatório.");
        
        if (!string.IsNullOrWhiteSpace(Cargo) && Cargo.Length > 100)
            erros.Add("O campo \"Cargo\" deve conter no máximo 100 caracteres.");

        if (!string.IsNullOrWhiteSpace(Empresa) && Empresa.Length > 100)
            erros.Add("O campo \"Empresa\" deve conter no máximo 100 caracteres.");

        return erros;
    }

    public override void Atualizar(Contato entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Email = entidadeAtualizada.Email;
        Telefone = entidadeAtualizada.Telefone;
        Cargo = entidadeAtualizada.Cargo;
        Empresa = entidadeAtualizada.Empresa;
    }
}
