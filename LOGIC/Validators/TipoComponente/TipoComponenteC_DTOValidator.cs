using FluentValidation;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIC.Validators
{
    public class TipoComponenteC_DTOValidator : AbstractValidator<DTOTipoComponenteCreate>
    {
        public TipoComponenteC_DTOValidator()
        {
            RuleFor(eec => eec.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres");
        }
    }
}
