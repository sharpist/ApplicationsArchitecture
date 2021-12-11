namespace CleanArchitecture.Core.Handlers;

public class EmployeeQueryHandler :
    IQueryHandler<GetEmployeesQuery, IEnumerable<ReadEmployeeDTO>>,
    IQueryHandler<GetEmployeeByIdQuery, ReadEmployeeDTO>
{
    private readonly IRepository<Employee> repository;
    private readonly IMapper mapper;

    public EmployeeQueryHandler(IRepository<Employee> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ReadEmployeeDTO>> Execute(GetEmployeesQuery _)
    {
        var employees = await repository.ReadAsync();
        return mapper.Map<IEnumerable<ReadEmployeeDTO>>(employees);
    }

    public async Task<ReadEmployeeDTO> Execute(GetEmployeeByIdQuery query)
    {
        var employee = await repository.FindAsync(query.Id);
        return mapper.Map<ReadEmployeeDTO>(employee);
    }
}
