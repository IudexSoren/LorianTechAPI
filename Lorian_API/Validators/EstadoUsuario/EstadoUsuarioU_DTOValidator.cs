﻿using FluentValidation;
using Lorian_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Validators
{
    public class EstadoUsuarioU_DTOValidator : AbstractValidator<DTOEstadoUsuarioUpdate>
    {
        public EstadoUsuarioU_DTOValidator()
        {
            RuleFor(euu => euu.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres");
        }
    }
}
