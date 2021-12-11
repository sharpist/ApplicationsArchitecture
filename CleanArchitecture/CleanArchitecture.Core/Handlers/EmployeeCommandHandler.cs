namespace CleanArchitecture.Core.Handlers;

public class EmployeeCommandHandler :
    ICommandHandler<PostEmployeeCommand>
{
    private readonly IRepository<Employee> repository;
    private readonly IMapper mapper;

    public EmployeeCommandHandler(IRepository<Employee> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task Execute(PostEmployeeCommand command)
    {
        var employee = mapper.Map<Employee>(command.Model);
        await repository.CreateAsync(employee);
    }
}
