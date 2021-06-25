using FluentValidation;
using Lorian_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Validators.EstadoMensaje
{
    public class EstadoMensajeC_DTOValidator : AbstractValidator<DTOEstadoMensajeCreate>
    {
        public EstadoMensajeC_DTOValidator()
        {
            RuleFor(emc => emc.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres");
        }
    }
}
