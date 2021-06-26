using FluentValidation;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIC.Validators
{
    public class CaracteristicaC_DTOValidator : AbstractValidator<DTOCaracteristicaCreate>
    {
        public CaracteristicaC_DTOValidator()
        {
            RuleFor(cc => cc.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres");

            RuleFor(cc => cc.Valor)
                .MaximumLength(100).WithMessage("El valor no debe exceder los 100 caracteres");
        }
    }
}
