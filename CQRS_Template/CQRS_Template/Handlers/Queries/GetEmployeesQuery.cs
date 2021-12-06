namespace CQRS_Template.Handlers.Queries;

public record class GetEmployeesQuery() : IQuery<EmployeeModel[]>;
