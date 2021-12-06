var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore(builder);
builder.Services.AddScoped<ICommandHandler<PostEmployeeCommand>, EmployeeCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetEmployeesQuery, EmployeeModel[]>, EmployeeQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetEmployeeByIdQuery, EmployeeModel>, EmployeeQueryHandler>();
builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

await using var app = builder.Build();

// configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger().UseSwaggerUI();
    await app.UseDbInitializerAsync();
}

app.UseHttpsRedirection();

// HTTP verbs
app.MapGet("/api/employee", async (IQueryDispatcher dispatcher) =>
{
    var query = new GetEmployeesQuery();
    var employees = await dispatcher.Execute<GetEmployeesQuery, EmployeeModel[]>(query);

    return Results.Ok(employees);
}).WithName("GetEmployees");

app.MapGet("/api/employee/{id}", async (IQueryDispatcher dispatcher, int id) =>
{
    var query = new GetEmployeeByIdQuery(id);
    var employee = await dispatcher.Execute<GetEmployeeByIdQuery, EmployeeModel>(query);

    return Results.Ok(employee);
}).WithName("GetEmployeeById");

app.MapPost("/api/employee", async (ICommandDispatcher dispatcher, EmployeeModel model) =>
{
    var command = new PostEmployeeCommand(model.Name, model.Department);
    await dispatcher.Execute(command);

    return Results.Ok();
}).WithName("PostEmployee");

await app.RunAsync();
