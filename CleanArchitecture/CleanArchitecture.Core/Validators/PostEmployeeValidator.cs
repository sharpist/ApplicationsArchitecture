namespace CleanArchitecture.Core.Validators;

public class PostEmployeeValidator : AbstractValidator<PostEmployeeCommand>
{
    public PostEmployeeValidator()
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
