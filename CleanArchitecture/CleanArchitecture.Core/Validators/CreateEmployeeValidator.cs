namespace CleanArchitecture.Core.Validators;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {
        RuleFor(model => model.Model.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(model => model.Model.Department)
            .NotNull()
            .NotEmpty()
            .WithMessage("Department is required");
    }
}
