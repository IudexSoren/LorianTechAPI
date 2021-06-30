using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class TarjetaC_DTOValidator : AbstractValidator<DTOTarjetaCreate>
    {
        public TarjetaC_DTOValidator()
        {
            RuleFor(tc => tc.Numero)
                .NotEmpty().WithMessage("El número de tarjeta es un dato requerido")
                .Length(19).WithMessage("Número de tarjeta inválido")
                .Matches("[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}").WithMessage("Número de tarjeta inválido");

            RuleFor(tc => tc.FechaExpiracion)
                .NotEmpty().WithMessage("La fecha de expiración es un dato requerido");

            RuleFor(rc => rc.CVV)
                .NotEmpty().WithMessage("CVV es un dato requerido")
                .Length(3, 5).WithMessage("CVV debe ser de almenos 3 caracteres y no debe exceder los 5 caracteres")
                .Matches("[0-9]{3,5}").WithMessage("CVV inválido");

            RuleFor(cc => cc.IdUsuario)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El usuario es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await UsuarioService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Usuario inválido");

            RuleFor(cc => cc.IdTipoTarjeta)
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
