using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators.Tarjeta
{
    public class TarjetaU_DTOValidator : AbstractValidator<DTOTarjetaUpdate>
    {
        public TarjetaU_DTOValidator()
        {
            RuleFor(tc => tc.Numero)
                .NotEmpty().WithMessage("El número de tarjeta es un dato requerido")
                .Matches("([0-9]{4}-*){19}").WithMessage("Número de tarjeta inválido");

            RuleFor(tc => tc.FechaExpiracion)
                .NotEmpty().WithMessage("La fecha de expiración es un dato requerido");

            RuleFor(rc => rc.CVV)
                .NotEmpty().WithMessage("CVV es un dato requerido")
                .MaximumLength(100).WithMessage("CVV no debe exceder los 5 caracteres");

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
