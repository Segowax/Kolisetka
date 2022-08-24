using FluentValidation.Results;
using Kolisetka.Application.Exceptions.ExceptionObjects;
using System;

namespace Kolisetka.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationErrors ValidationErrors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = new();
            foreach (var error in validationResult.Errors)
            {
                ValidationErrors.Errors.Add(error.ErrorMessage);
            }
        }
    }
}
