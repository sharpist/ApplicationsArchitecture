namespace CleanArchitecture.Core.Validators;

public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeDTO>
{
    public DeleteEmployeeValidator()
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
    }
}
