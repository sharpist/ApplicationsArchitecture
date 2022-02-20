namespace CleanArchitecture.Core.Handlers.QueryHandlers.Employees;

public class EmployeeQueryHandler :
    IQueryHandler<ReadEmployeesQuery, IEnumerable<EmployeeDTO>>,
    IQueryHandler<ReadEmployeeByIdQuery, EmployeeDTO>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public EmployeeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<EmployeeDTO>> Execute(ReadEmployeesQuery _, CancellationToken cancellationToken = default)
    {
        var employees = await unitOfWork.GetRepository<Employee>().ReadAllAsync(x => x, cancellationToken: cancellationToken);
        return mapper.Map<EmployeeDTO[]>(employees);
    }

    public async Task<EmployeeDTO> Execute(ReadEmployeeByIdQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await unitOfWork.GetRepository<Employee>().FindAsync(query.Id, cancellationToken);
        return mapper.Map<EmployeeDTO>(employee);
    }
}
