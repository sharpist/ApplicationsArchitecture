namespace CleanArchitecture.Core.Features.Employees.Queries;

public sealed record class GetEmployeesQuery() : IQuery<IEnumerable<Employee>>;
