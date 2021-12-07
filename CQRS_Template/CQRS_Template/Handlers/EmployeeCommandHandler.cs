namespace CQRS_Template.Handlers;

public class EmployeeCommandHandler :
    ICommandHandler<PostEmployeeCommand>
{
    private readonly IRepository<EmployeeModel> repository;

    public EmployeeCommandHandler(IRepository<EmployeeModel> repository)
    {
        this.repository = repository;
    }

    public async Task Execute(PostEmployeeCommand command)
    {
        var employee = new EmployeeModel { Name = command.Name, Department = command.Department };
        await repository.CreateAsync(employee);
    }
}
