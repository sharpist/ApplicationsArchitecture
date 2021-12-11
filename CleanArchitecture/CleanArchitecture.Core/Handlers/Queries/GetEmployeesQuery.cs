namespace CleanArchitecture.Core.Handlers.Queries;

public record class GetEmployeesQuery() : IQuery<IEnumerable<ReadEmployeeDTO>>;
