﻿using FluentValidation;
using ENTITIES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIC.Validators
{
    public class EstadoEnvioU_DTOValidator : AbstractValidator<DTOEstadoEnvioUpdate>
    {
        public EstadoEnvioU_DTOValidator()
        {
            RuleFor(eeu => eeu.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres");
        }
    }
}
