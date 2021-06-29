using ENTITIES.DTOs;
using FluentValidation;
using LOGIC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOGIC.Validators
{
    public class UsuarioC_DTOValidator : AbstractValidator<DTOUsuarioCreate>
    {
        public UsuarioC_DTOValidator()
        {
            RuleFor(uc => uc.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El correo electrónico es un dato requerido")
                .EmailAddress().WithMessage("Dirección de correo electrónico inválida")
                .MaximumLength(150).WithMessage("La dirección de correo electrónico no debe exceder los 150 caracteres. Actual: {PropertyValue}")
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

            RuleFor(uc => uc.Nombre)
                .NotEmpty().WithMessage("El nombre es un dato requerido")
                .MaximumLength(100).WithMessage("El nombre no debe exceder los 100 caracteres. Actual: {PropertyValue}");
            
            RuleFor(uc => uc.Apellido)
                .NotEmpty().WithMessage("El apellido es un dato requerido")
                .MaximumLength(100).WithMessage("El apellido no debe exceder los 100 caracteres. Actual: {PropertyValue}");

            RuleFor(uc => uc.Clave)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La contraseña es un dato requerido")
                .Length(8, 32).WithMessage("La contraseña debe ser de almenos 8 caracteres y no debe exceder los 32 caracteres");

            RuleFor(uc => uc.ConfirmarClave)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe confirmar su contraseña")
                .Length(8, 32).WithMessage("La contraseña de confirmación debe ser de almenos 8 caracteres y no debe exceder los 32 caracteres")
                .Must((uc, confClave) => confClave.Equals(uc.Clave)).WithMessage("Las contraseñas no coinciden");

            RuleFor(uc => uc.IdRol)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El rol es un dato requerido")
                .MustAsync(async (id, cancellation) =>
                {
                    var res = await RolService.ReadSimple(id);

                    return res != null;
                })
                .WithMessage("Rol inválido");
        }
    }
}
