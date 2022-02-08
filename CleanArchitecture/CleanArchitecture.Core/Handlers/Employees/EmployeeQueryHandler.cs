namespace CleanArchitecture.Core.Handlers.Employees;

public class EmployeeQueryHandler :
    IQueryHandler<GetEmployeesQuery, IEnumerable<Employee>>,
    IQueryHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IRepository<Employee> repository;

    public EmployeeQueryHandler(IRepository<Employee> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Employee>> Execute(GetEmployeesQuery _, CancellationToken cancellationToken = default)
    {
        return await repository.ReadAsync(cancellationToken);
    }

    public async Task<Employee> Execute(GetEmployeeByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await repository.FindAsync(query.Id, cancellationToken);
    }
}
