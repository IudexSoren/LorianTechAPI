using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class UsuarioU_DTOValidator : AbstractValidator<DTOUsuarioUpdate>
    {
        public UsuarioU_DTOValidator()
        {
            RuleFor(uu => uu.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El correo electrónico es un dato requerido")
                .EmailAddress().WithMessage("Dirección de correo electrónico inválida")
                .MaximumLength(150).WithMessage("La direuuión de correo electrónico no debe exceder los 150 caracteres. Actual: {PropertyValue}")
                .MustAsync(async (email, cancellation) =>
                {
                    foreach (var usuario in await UsuarioService.ReadAllSimple())
                    {
                        if (usuario.Email.Equals(email))
                        {
                            return false;
                        }
                    }
                    return true;
                })
                .WithMessage("El correo electrónico: {PropertyValue}, ya está registrado");

            RuleFor(uu => uu.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres. Actual: {PropertyValue}");

            RuleFor(uu => uu.Apellido)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres. Actual: {PropertyValue}");

            RuleFor(uu => uu.Clave)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La contraseña es un dato requerido")
                .Length(8, 32).WithMessage("La contraseña debe ser de almenos 8 caracteres y no debe exceder los 32 caracteres");

            RuleFor(uu => uu.ConfirmarClave)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe confirmar su contraseña")
                .Length(8, 32).WithMessage("La contraseña de confirmación debe ser de almenos 8 caracteres y no debe exceder los 32 caracteres")
                .Matches(uu => uu.Clave).WithMessage("Las contraseñas no coinciden");

            RuleFor(uu => uu.EmailVerificado)
                .NotEmpty().WithMessage("Debe indicar si el correo electrónico está verificado");

            RuleFor(uu => uu.IdEstadoUsuario)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El estado del usuario es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await EstadoUsuarioService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Estado de usuario inválido");
        }
    }
}
