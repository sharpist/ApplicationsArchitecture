namespace CleanArchitecture.Core.Handlers.Employees;

public class EmployeeCommandHandler :
    ICommandHandler<PostEmployeeCommand>,
    ICommandHandler<PutEmployeeCommand>,
    ICommandHandler<DeleteEmployeeCommand>
{
    private readonly IRepository<Employee> repository;
    private readonly IMapper mapper;

    public EmployeeCommandHandler(IRepository<Employee> repository, IMapper mapper)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Execute(PostEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var employee = mapper.Map<Employee>(command);
        await repository.CreateAsync(employee, cancellationToken);
    }

    public async Task Execute(PutEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var employee = mapper.Map<Employee>(command);
        await repository.UpdateAsync(employee, cancellationToken);
    }

    public async Task Execute(DeleteEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(command.Id, cancellationToken);
    }
}
