namespace CleanArchitecture.Core.Features.Employees.Queries;

public sealed record class GetEmployeeByIdQuery(int Id) : IQuery<Employee>;
