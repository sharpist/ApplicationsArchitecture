namespace CleanArchitecture.Core.Validators;

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(model => model.Model.Id)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Id is required");
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
