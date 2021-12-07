namespace CQRS_Template.Handlers;

public class EmployeeQueryHandler :
    IQueryHandler<GetEmployeesQuery, EmployeeModel[]>,
    IQueryHandler<GetEmployeeByIdQuery, EmployeeModel>
{
    private readonly IRepository<EmployeeModel> repository;

    public EmployeeQueryHandler(IRepository<EmployeeModel> repository)
    {
        this.repository = repository;
    }

    public async Task<EmployeeModel[]> Execute(GetEmployeesQuery _)
    {
        return (EmployeeModel[])await repository.ReadAsync();
    }

    public async Task<EmployeeModel> Execute(GetEmployeeByIdQuery query)
    {
        return await repository.ReadAsync(query.EmployeeId);
    }
}
