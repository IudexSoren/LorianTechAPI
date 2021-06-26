using FluentValidation;
using ENTITIES.DTOs;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class ComponenteU_DTOValidator : AbstractValidator<DTOComponenteUpdate>
    {
        public ComponenteU_DTOValidator()
        {
            RuleFor(cc => cc.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(250).WithMessage("El nombre no debe exceder los 250 caracteres. Actual: {PropertyValue}");

            RuleFor(cc => cc.Inventario)
                .NotEmpty().WithMessage("El inventario es un dato requerido")
                .GreaterThanOrEqualTo(0).WithMessage("El inventario no puede ser un número negativo");

            RuleFor(cc => cc.Garantia)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("La garantía no debe exceder los 100 caracteres. Actual: {PropertyValue}");

            RuleFor(cc => cc.IdTipoComponente)
                .NotEmpty().WithMessage("El tipo de componente es un dato requerido")
                .MustAsync(async (id, cancellation) => {
                    var res = await ElementExistsService.TipoComponenteExists(id);

                    return res != null;
                })
                .WithMessage("Tipo de componente inválido");

            RuleFor(cc => cc.IdMarca)
                .NotEmpty().WithMessage("La marca es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await ElementExistsService.MarcaExists(id);

                    return res != null;
                })
                .WithMessage("Marca inválida");

            RuleFor(cc => cc.IdEstadoComponente)
                .NotEmpty().WithMessage("El estado del componente es un dato requerido")
                .MustAsync(async (id, cancellation) => {
                    var res = await ElementExistsService.EstadoComponenteExists(id);

                    return res != null;
                })
                .WithMessage("Estado de componente inválido");

            RuleForEach(cc => cc.IdsCaracteristicas)
                .MustAsync(async (id, cancellation) => {
                    var res = await ElementExistsService.CaracteristicaExists(id);

                    return res != null;
                })
                .WithMessage("Caracteristica inválida");

            RuleForEach(cc => cc.IdsPromociones)
                .MustAsync(async (id, cancellation) => {
                    var res = await ElementExistsService.PromocionExists(id);

                    return res != null;
                })
                .WithMessage("Promoción inválida");
        }
    }
}
