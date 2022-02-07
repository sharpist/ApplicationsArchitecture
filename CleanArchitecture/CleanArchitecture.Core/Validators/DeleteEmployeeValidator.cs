namespace CleanArchitecture.Core.Validators;

public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeValidator()
    {
        RuleFor(model => model.Id)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Id is required");
    }
}
