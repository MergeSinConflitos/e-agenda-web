using eAgendaWeb.WebApplication.ModuloCompromisso.Dominio;
using eAgendaWeb.WebApplication.ModuloContato.Dominio;
using FluentResults;

namespace eAgendaWeb.WebApplication.ModuloContato.Aplicacao;

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

    public Result Editar(EditarContatoDto dto)
    {
        Result resultadoDuplicado = ValidarDuplicado(dto.Email, dto.Telefone, dto.Id);

        if (resultadoDuplicado.IsFailed)
            return resultadoDuplicado;

        Contato contatoAtualizado = new Contato(dto.Nome, dto.Email, dto.Telefone, dto.Cargo, dto.Empresa);

        Result resultadoValidacao = ValidarEntidade(contatoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioContato.Editar(dto.Id, contatoAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Contato não encontrado.");

        return Result.Ok().WithSuccess("Contato editado com sucesso!");
    }

    public Result Excluir(Guid id)
    {
        Contato? contato = repositorioContato.SelecionarPorId(id);

        if (contato == null)
            return Result.Fail("Contato não encontrado.");

        if (ExisteCompromissoVinculado(id))
            return Result.Fail("Não é possível excluir um contato que possui compromissos vinculados.");

        repositorioContato.Excluir(id);

        return Result.Ok().WithSuccess("Contato excluído com sucesso!"); 
    }
    
    private bool ExisteCompromissoVinculado(Guid contatoId)
    {
        return repositorioCompromisso
            .SelecionarTodos()
            .Any(c => c.Contato?.Id == contatoId);
    }
    
    public List<ListarContatosDto> SelecionarTodos()
    {
        return repositorioContato
            .SelecionarTodos()
            .Select(MapearParaListarDto)
            .ToList();
    }

    public Result<DetalhesContatoDto> SelecionarPorId(Guid id)
    {
        Contato? contato = repositorioContato.SelecionarPorId(id);

        if (contato == null)
            return Result.Fail("Contato não encontrado.");

        return Result.Ok(MapearParaDetalhesDto(contato));
    }

    private static DetalhesContatoDto MapearParaDetalhesDto(Contato contato)
    {
        return new DetalhesContatoDto(
            contato.Id,
            contato.Nome,
            contato.Email,
            contato.Telefone,
            contato.Cargo,
            contato.Empresa
        );
    }

    private static ListarContatosDto MapearParaListarDto(Contato contato)
    {
        return new ListarContatosDto(
            contato.Id,
            contato.Nome,
            contato.Email,
            contato.Telefone,
            contato.Cargo,
            contato.Empresa
        );
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
