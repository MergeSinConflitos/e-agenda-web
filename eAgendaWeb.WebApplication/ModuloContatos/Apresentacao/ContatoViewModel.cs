using System.ComponentModel.DataAnnotations;

namespace eAgendaWeb.WebApplication.ModuloContatos.Apresentacao;

public record ListarContatosViewModel(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string? Cargo,
    string? Empresa
);

public record CadastrarContatosViewModel(
    
    [Required(ErrorMessage ="O campo \"Nome\" é obrigatório")]
    [StringLength(100,MinimumLength =2,ErrorMessage ="O nome deve ter entre 2 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Email\" é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    string Email,

    [Required(ErrorMessage = "O campo Telefone deve ser preenchido")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "Telefone inválido")]
    string Telefone,

    [StringLength(100, ErrorMessage = "O campo \"Cargo\" deve conter no máximo 100 caracteres.")]
    string? Cargo,

    [StringLength(100, ErrorMessage = "O campo \"Empresa\" deve conter no máximo 100 caracteres.")]
    string? Empresa

);

public record EditarContatoViewModel(
    Guid Id,

    [Required(ErrorMessage ="O campo \"Nome\" é obrigatório")]
    [StringLength(100,MinimumLength =2,ErrorMessage ="O nome deve ter entre 2 e 100 caracteres")]
    string Nome,

    [Required(ErrorMessage ="O campo \"Email\" é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    string Email,

    [Required(ErrorMessage = "O campo Telefone deve ser preenchido")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "Telefone inválido")]
    string Telefone,

    [StringLength(100, ErrorMessage = "O campo \"Cargo\" deve conter no máximo 100 caracteres.")]
    string? Cargo,

    [StringLength(100, ErrorMessage = "O campo \"Empresa\" deve conter no máximo 100 caracteres.")]
    string? Empresa
);

public record ExcluirContatoViewModel(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string? Cargo,
    string? Empresa
);