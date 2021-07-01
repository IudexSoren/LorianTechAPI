using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators.Factura
{
    public class FacturaC_DTOValidator : AbstractValidator<DTOFacturaCreate>
    {
        public FacturaC_DTOValidator()
        {
            RuleFor(tc => tc.IdUsuario)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El usuario es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await UsuarioService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Usuario inválido");

            RuleFor(tc => tc.IdTarjeta)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La tarjeta con la que se realizará el pago es un dato requerido")
                .MustAsync(async (fc, id, cancellation) =>
                {
                    var res = await TarjetaService.ReadSimple(id);
                    if (res != null)
                    {
                        if (res.IdUsuario == fc.IdUsuario)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                })
                .WithMessage("Tarjeta inválida");
        }
    }
}
