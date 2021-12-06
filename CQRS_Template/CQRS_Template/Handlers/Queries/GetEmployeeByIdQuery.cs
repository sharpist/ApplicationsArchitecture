namespace CQRS_Template.Handlers.Queries;

public record class GetEmployeeByIdQuery(int EmployeeId) : IQuery<EmployeeModel>;
