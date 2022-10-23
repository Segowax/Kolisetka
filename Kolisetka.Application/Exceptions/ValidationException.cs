using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Kolisetka.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            Errors = new();
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}
