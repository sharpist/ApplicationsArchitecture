namespace CleanArchitecture.Core.Validators;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDTO>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(model => model.Id)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Id is required");
        RuleFor(model => model.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(model => model.Department)
            .NotNull()
            .NotEmpty()
            .WithMessage("Department is required");
        RuleFor(model => model.Modified)
            .NotNull()
            .NotEmpty()
            .WithMessage("Modification time is required");
    }
}
