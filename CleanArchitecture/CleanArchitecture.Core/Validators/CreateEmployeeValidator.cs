namespace CleanArchitecture.Core.Validators;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
{
    public CreateEmployeeValidator()
    {
        RuleFor(model => model.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(model => model.Department)
            .NotNull()
            .NotEmpty()
            .WithMessage("Department is required");
    }
}
