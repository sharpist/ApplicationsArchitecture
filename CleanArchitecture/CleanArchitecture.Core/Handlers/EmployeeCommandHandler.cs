namespace CleanArchitecture.Core.Handlers;

public class EmployeeCommandHandler :
    ICommandHandler<PostEmployeeCommand>
{
    private readonly IRepository<Employee> repository;
    private readonly IValidator<CreateOrUpdateEmployeeDTO> validator;
    private readonly IMapper mapper;

    public EmployeeCommandHandler(IRepository<Employee> repository, IValidator<CreateOrUpdateEmployeeDTO> validator, IMapper mapper)
    {
        this.repository = repository;
        this.validator = validator;
        this.mapper = mapper;
    }

    public async Task Execute(PostEmployeeCommand command)
    {
        var model = command.Model;
        var result = validator.Validate(model);
        //logger.LogInformation($"PostEmployee Validation result: {result}");

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
            throw new InvalidRequestBodyException
            {
                Errors = errors
            };
        }

        var employee = mapper.Map<Employee>(command.Model);
        await repository.CreateAsync(employee);
    }
}
