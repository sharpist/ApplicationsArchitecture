namespace CleanArchitecture.Core.Handlers.Queries;

public sealed record class GetEmployeeByIdQuery(int Id) : IQuery<ReadEmployeeDTO>;
