using FluentValidation;
using Lorian_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Validators
{
    public class CaracteristicaU_DTOValidator : AbstractValidator<DTOCaracteristicaUpdate>
    {
        public CaracteristicaU_DTOValidator()
        {
            RuleFor(cu => cu.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres");

            RuleFor(cu => cu.Valor)
                .MaximumLength(100).WithMessage("El valor no debe exceder los 100 caracteres");
        }
    }
}
