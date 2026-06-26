using System;
using System.ComponentModel.DataAnnotations;

namespace eAgendaWeb.WebApplication.ModuloDeCategoria.Aplicacao;

public record ListarCategoriasDto(
    Guid Id,
    string Titulo
);

public record CadastrarCategoriaDto(
    [Required(ErrorMessage ="O campo Titulo deve ser preenchido")]
    [StringLength(100, MinimumLength =2,ErrorMessage ="O titulo deve ter entre 100 e 2 caracteres")]
    string Titulo
);

public record EditarCategoriaDto(
    Guid Id,

    [Required(ErrorMessage ="O campo Titulo deve ser preenchido")]
    [StringLength(100, MinimumLength =2,ErrorMessage ="O titulo deve ter entre 100 e 2 caracteres")]
    string Titulo
);

public record ExcluirCategoriaDto(
    Guid Id,
    string Titulo
);