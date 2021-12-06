namespace CQRS_Template.Handlers;

public class EmployeeCommandHandler : ICommandHandler<PostEmployeeCommand>
{
    private readonly EmployeeDbContext context;

    public EmployeeCommandHandler(EmployeeDbContext context)
    {
        this.context = context;
    }

    public async Task Execute(PostEmployeeCommand command)
    {
        var employee = new EmployeeModel { Name = command.Name, Department = command.Department };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
    }
}
