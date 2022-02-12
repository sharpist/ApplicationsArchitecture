namespace CleanArchitecture.Core.Features.Employees.Queries;

public sealed record class ReadEmployeeByIdQuery(int Id) : IQuery<EmployeeDTO>;
