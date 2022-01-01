namespace CleanArchitecture.Core.Validators;

public class CreateOrUpdateEmployeeDTOValidator : AbstractValidator<CreateOrUpdateEmployeeDTO>
{
    public CreateOrUpdateEmployeeDTOValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(model => model.Department)
            .NotEmpty()
            .WithMessage("Department is required");
    }
}
