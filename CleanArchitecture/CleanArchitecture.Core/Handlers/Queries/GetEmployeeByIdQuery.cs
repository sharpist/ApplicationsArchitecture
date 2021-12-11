namespace CleanArchitecture.Core.Handlers.Queries;

public record class GetEmployeeByIdQuery(int Id) : IQuery<ReadEmployeeDTO>;
