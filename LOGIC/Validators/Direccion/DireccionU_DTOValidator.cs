using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class DireccionU_DTOValidator : AbstractValidator<DTODireccionUpdate>
    {
        public DireccionU_DTOValidator()
        {
            RuleFor(du => du.IdUsuario)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El usuario es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await UsuarioService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Usuario inválido");

            RuleFor(du => du.Descripcion)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La dirección es un dato requerido")
                .Length(10, 500).WithMessage("Debe especificar mejor su dirección sin exceder los 500 caracteres");
        }
    }
}
