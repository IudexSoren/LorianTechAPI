using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class TarjetaU_DTOValidator : AbstractValidator<DTOTarjetaUpdate>
    {
        public TarjetaU_DTOValidator()
        {
            RuleFor(tu => tu.Numero)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El número de tarjeta es un dato requerido")
                .Matches("([0-9]{4}-*){19}").WithMessage("Número de tarjeta inválido");

            RuleFor(tu => tu.FechaExpiracion)
                .NotEmpty().WithMessage("La fecha de expiración es un dato requerido");

            RuleFor(tu => tu.CVV)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("CVV es un dato requerido")
                .Length(3, 5).WithMessage("CVV debe ser de almenos 3 caracteres y no debe exceder los 5 caracteres")
                .Matches("[0-9]{3,5}").WithMessage("CVV inválido");

            RuleFor(tu => tu.IdUsuario)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El usuario es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await UsuarioService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Usuario inválido");

            RuleFor(tu => tu.IdTipoTarjeta)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El tipo de tarjeta es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await TipoTarjetaService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Tipo de tarjeta inválido");
        }
    }
}
