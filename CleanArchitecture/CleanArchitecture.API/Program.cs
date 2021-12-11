var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore();
builder.Services.AddCQRS();

await using var app = builder.Build();

// configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger().UseSwaggerUI();
    //await app.UseDbInitializerAsync();
}

app.UseHttpsRedirection();

// HTTP verbs
app.MapGet("/api/employee", async (IQueryDispatcher dispatcher) =>
{
    var query = new GetEmployeesQuery();
    var employees = await dispatcher.Execute<GetEmployeesQuery, IEnumerable<ReadEmployeeDTO>>(query);

    return Results.Ok(employees);
}).WithName("GetEmployees");

app.MapGet("/api/employee/{id}", async (IQueryDispatcher dispatcher, int id) =>
{
    var query = new GetEmployeeByIdQuery(id);
    var employee = await dispatcher.Execute<GetEmployeeByIdQuery, ReadEmployeeDTO>(query);

    return Results.Ok(employee);
}).WithName("GetEmployeeById");

app.MapPost("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] CreateEmployeeDTO model) =>
{
    var command = new PostEmployeeCommand(model);
    await dispatcher.Execute(command);

    return Results.Ok();
}).WithName("PostEmployee");

await app.RunAsync();
