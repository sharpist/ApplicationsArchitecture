namespace CleanArchitecture.Core.Handlers.CommandHandlers.Employees;

public class EmployeeCommandHandler :
    ICommandHandler<CreateEmployeeCommand>,
    ICommandHandler<UpdateEmployeeCommand>,
    ICommandHandler<DeleteEmployeeCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public EmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Execute(CreateEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var employee = mapper.Map<Employee>(command.Model);
        await unitOfWork.GetRepository<Employee>().CreateAsync(employee, cancellationToken);
        await unitOfWork.CommitAsync();
    }

    public async Task Execute(UpdateEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        var employee = mapper.Map<Employee>(command.Model);
        await unitOfWork.GetRepository<Employee>().UpdateAsync(employee, cancellationToken);
        await unitOfWork.CommitAsync();
    }

    public async Task Execute(DeleteEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        await unitOfWork.GetRepository<Employee>().DeleteAsync(command.Id, cancellationToken);
        await unitOfWork.CommitAsync();
    }
}
