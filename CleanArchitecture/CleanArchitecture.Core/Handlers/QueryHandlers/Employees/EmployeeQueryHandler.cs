namespace CleanArchitecture.Core.Handlers.QueryHandlers.Employees;

public class EmployeeQueryHandler :
    IQueryHandler<ReadEmployeesQuery, IEnumerable<EmployeeDTO>>,
    IQueryHandler<ReadEmployeeByIdQuery, EmployeeDTO>
{
    private readonly IRepository<Employee> repository;
    private readonly IMapper mapper;

    public EmployeeQueryHandler(IRepository<Employee> repository, IMapper mapper)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<EmployeeDTO>> Execute(ReadEmployeesQuery _, CancellationToken cancellationToken = default)
    {
        var employees = await repository.ReadAllAsync(x => x, cancellationToken: cancellationToken);
        return mapper.Map<EmployeeDTO[]>(employees);
    }

    public async Task<EmployeeDTO> Execute(ReadEmployeeByIdQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await repository.FindAsync(query.Id, cancellationToken);
        return mapper.Map<EmployeeDTO>(employee);
    }
}
