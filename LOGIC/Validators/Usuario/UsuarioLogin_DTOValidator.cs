using ENTITIES.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class UsuarioLogin_DTOValidator : AbstractValidator<DTOUsuarioLogin>
    {
        public UsuarioLogin_DTOValidator()
        {
            RuleFor(ul => ul.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El correo electrónico es un dato requerido")
                .EmailAddress().WithMessage("Dirección de correo electrónico inválida")
                .MaximumLength(150).WithMessage("La dirección de correo electrónico no debe exceder los 150 caracteres. Actual: {PropertyValue}");

            RuleFor(ul => ul.Clave)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La contraseña es un dato requerido")
                .Length(8, 32).WithMessage("La contraseña debe ser de almenos 8 caracteres y no debe exceder los 32 caracteres");
        }
    }
}
