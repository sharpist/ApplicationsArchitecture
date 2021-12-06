namespace CQRS_Template.Handlers;

public class EmployeeQueryHandler :
    IQueryHandler<GetEmployeesQuery, EmployeeModel[]>,
    IQueryHandler<GetEmployeeByIdQuery, EmployeeModel>
{
    private readonly EmployeeDbContext context;

    public EmployeeQueryHandler(EmployeeDbContext context)
    {
        this.context = context;
    }

    public async Task<EmployeeModel[]> Execute(GetEmployeesQuery _)
    {
        var employees = await context.Employees.ToArrayAsync();
        return employees;
    }

    public async Task<EmployeeModel> Execute(GetEmployeeByIdQuery query)
    {
        var employee = await context.Employees.FindAsync(query.EmployeeId);
        return employee;
    }
}
