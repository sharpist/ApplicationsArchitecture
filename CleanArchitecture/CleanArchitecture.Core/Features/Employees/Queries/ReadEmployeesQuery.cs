namespace CleanArchitecture.Core.Features.Employees.Queries;

public sealed record class ReadEmployeesQuery() : IQuery<IEnumerable<EmployeeDTO>>;
