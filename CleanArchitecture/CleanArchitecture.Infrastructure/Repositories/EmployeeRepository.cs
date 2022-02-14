namespace CleanArchitecture.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DatabaseContext<Employee> context) : base(context)
    {
    }
}
