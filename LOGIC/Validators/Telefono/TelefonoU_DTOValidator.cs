using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class TelefonoU_DTOValidator : AbstractValidator<DTOTelefonoUpdate>
    {
        public TelefonoU_DTOValidator()
        {
            RuleFor(tu => tu.IdUsuario)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El usuario es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await UsuarioService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Usuario inválido");

            RuleFor(tu => tu.Numero)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El número de teléfono es un dato requerido")
                .Length(8).WithMessage("Número de teléfono inválido")
                .Matches("[0-9]{8}").WithMessage("Número de teléfono inválido");
        }
    }
}
