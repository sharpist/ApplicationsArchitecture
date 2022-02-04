namespace CleanArchitecture.Core.Handlers.Queries;

public sealed record class GetEmployeesQuery() : IQuery<IEnumerable<ReadEmployeeDTO>>;
