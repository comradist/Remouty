using FluentValidation;

namespace OutOfOffice.Shared.DTOs.Employee.Validators;

public class INoteDtoValidator : AbstractValidator<IEmployeeDto>
{
    public INoteDtoValidator()
    {
        RuleFor(p => p.FullName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 30 characters");

        RuleFor(p => p.OutOfOfficeBalance)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();
    }
}