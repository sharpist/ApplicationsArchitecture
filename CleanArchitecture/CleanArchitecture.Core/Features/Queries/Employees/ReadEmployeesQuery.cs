namespace CleanArchitecture.Core.Features.Queries.Employees;

public sealed record class ReadEmployeesQuery() : IQuery<IEnumerable<EmployeeDTO>>;
