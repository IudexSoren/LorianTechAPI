﻿using FluentValidation;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIC.Validators
{
    public class EstadoComponenteC_DTOValidator : AbstractValidator<DTOEstadoComponenteCreate>
    {
        public EstadoComponenteC_DTOValidator()
        {
            RuleFor(ecc => ecc.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres");
        }
    }
}
