namespace CleanArchitecture.Core.Features.Queries.Employees;

public sealed record class ReadEmployeeByIdQuery(int Id) : IQuery<EmployeeDTO>;
