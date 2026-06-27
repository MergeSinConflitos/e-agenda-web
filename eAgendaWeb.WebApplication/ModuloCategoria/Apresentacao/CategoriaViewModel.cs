using System;
using System.ComponentModel.DataAnnotations;

namespace eAgendaWeb.WebApplication.ModuloDeCategoria.Apresentacao;

public record ListarCategoriaViewModel(
    Guid Id,
    string Titulo
);

public record CadastrarCategoriaViewModel(
    [Required(ErrorMessage ="O campo \"Titulo\"deve ser preenchido")]
    [StringLength(100,MinimumLength =2,ErrorMessage ="O titulo deve ter entre 2 e 100 caractres")]
    string Titulo
);

public record EditarCategoriaViewModel(
    Guid Id,
    [Required(ErrorMessage ="O campo \"Titulo\"deve ser preenchido")]
    [StringLength(100,MinimumLength =2,ErrorMessage ="O titulo deve ter entre 2 e 100 caractres")]
    string Titulo
);

public record ExcluirCategoriaViewModel(
    Guid Id,
    string Titulo
);