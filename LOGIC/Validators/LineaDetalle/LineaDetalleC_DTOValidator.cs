using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators.LineaDetalle
{
    public class LineaDetalleC_DTOValidator : AbstractValidator<DTOLineaDetalleCreate>
    {
        public LineaDetalleC_DTOValidator()
        {
            RuleFor(ldc => ldc.Cantidad)
                .NotEmpty().WithMessage("La cantidad es un dato requerido")
                .GreaterThan(-1).WithMessage("La cantidad no puede ser negativa");

            RuleFor(tc => tc.IdComponente)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El componente es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await ComponenteService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Componente inválido");
        }
    }
}
