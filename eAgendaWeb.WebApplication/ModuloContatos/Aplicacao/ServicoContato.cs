using eAgendaWeb.WebApplication.ModuloCompromisso.Dominio;
using eAgendaWeb.WebApplication.ModuloContatos.Dominio;
using FluentResults;

namespace eAgendaWeb.WebApplication.ModuloContatos.Aplicacao;

public class ServicoContato
{
    private readonly IRepositorioContato repositorioContato;
    private readonly IRepositorioCompromisso repositorioCompromisso;

    public ServicoContato(
        IRepositorioContato repositorioContato,
        IRepositorioCompromisso repositorioCompromisso)
    {
        this.repositorioContato = repositorioContato;
        this.repositorioCompromisso = repositorioCompromisso;
    }

     public Result Cadastrar(CadastrarContatoDto dto)
    {
        Result resultadoDuplicado = ValidarDuplicado(dto.Email, dto.Telefone);

        if (resultadoDuplicado.IsFailed)
            return resultadoDuplicado;

        Contato novoContato = new Contato(dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);

        Result resultadoValidacao = ValidarEntidade(novoContato);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioContato.Cadastrar(novoContato);

        return Result.Ok().WithSuccess("Contato cadastrado com sucesso!");
    }

    private Result ValidarEntidade(Contato contato)
    {
        List<string> erros = contato.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private Result ValidarDuplicado(string email, string telefone, Guid? idIgnorado = null)
    {
        List<Contato> contatos = repositorioContato.SelecionarTodos();

        bool emailDuplicado = contatos.Any(c =>
            c.Id != idIgnorado &&
            string.Equals(c.Email, email, StringComparison.OrdinalIgnoreCase));

        if (emailDuplicado)
            return Falha(nameof(email), "Já existe um contato cadastrado com este e-mail.");

        bool telefoneDuplicado = contatos.Any(c =>
            c.Id != idIgnorado &&
            c.Telefone == telefone);

        if (telefoneDuplicado)
            return Falha(nameof(telefone), "Já existe um contato cadastrado com este telefone.");

        return Result.Ok();
    }

    private Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
