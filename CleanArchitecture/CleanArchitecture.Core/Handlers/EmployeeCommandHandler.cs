namespace CleanArchitecture.Core.Handlers;

public class EmployeeCommandHandler :
    ICommandHandler<PostEmployeeCommand>,
    ICommandHandler<PutEmployeeCommand>,
    ICommandHandler<DeleteEmployeeCommand>
{
    private readonly IRepository<Employee> repository;
    private readonly IValidatorFactory validatorFactory;
    private readonly IMapper mapper;
    private readonly ILogger<EmployeeCommandHandler> logger;

    public EmployeeCommandHandler(IRepository<Employee> repository, IValidatorFactory validatorFactory, IMapper mapper, ILogger<EmployeeCommandHandler> logger)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Execute(PostEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var model = command.Model;

        var validator = validatorFactory.GetValidator<CreateEmployeeDTO>();
        var result = await validator.ValidateAsync(model, cancellationToken);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("PostEmployee Validation result: {result}", result);
        }

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
            throw new InvalidRequestBodyException
            {
                Errors = errors
            };
        }

        var employee = mapper.Map<Employee>(model);
        await repository.CreateAsync(employee, cancellationToken);
    }

    public async Task Execute(PutEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var model = command.Model;

        var validator = validatorFactory.GetValidator<UpdateEmployeeDTO>();
        var result = await validator.ValidateAsync(model, cancellationToken);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("PutEmployee Validation result: {result}", result);
        }

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
            throw new InvalidRequestBodyException
            {
                Errors = errors
            };
        }

        var employee = mapper.Map<Employee>(model);
        await repository.UpdateAsync(employee, cancellationToken);
    }

    public async Task Execute(DeleteEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var model = command.Model;

        var validator = validatorFactory.GetValidator<DeleteEmployeeDTO>();
        var result = await validator.ValidateAsync(model, cancellationToken);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("DeleteEmployee Validation result: {result}", result);
        }

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
            throw new InvalidRequestBodyException
            {
                Errors = errors
            };
        }

        var employee = mapper.Map<Employee>(model);
        await repository.DeleteAsync(employee, cancellationToken);
    }
}
