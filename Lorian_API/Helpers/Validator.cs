using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lorian_API.Helpers
{
    public static class Validator
    {
        public static List<string> ListarErrores(List<ValidationFailure> failures)
        {
            List<string> errores = new List<string>();
            foreach (ValidationFailure failure in failures)
            {
                errores.Add($"{ failure.ErrorMessage }");
            }

            return errores;
        }
    }
}
