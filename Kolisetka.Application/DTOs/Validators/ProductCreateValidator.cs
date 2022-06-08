﻿using FluentValidation;

namespace Kolisetka.Application.DTOs.Validators
{
    class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator()
        {
            RuleFor(prop => prop.Category)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(prop => prop.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(prop => prop.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(prop => prop.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}