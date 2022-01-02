namespace CleanArchitecture.Core.Validators;

public class ValidatorFactory : ValidatorFactoryBase
{
    private readonly IServiceProvider provider;

    public ValidatorFactory(IServiceProvider provider)
    {
        this.provider = provider;
    }

    public override IValidator? CreateInstance(Type validatorType)
    {
        using var scope = provider.CreateScope();
        var validator = scope.ServiceProvider.GetRequiredService(validatorType) as IValidator;

        return validator;
    }
}
