var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore();
builder.Services.AddCQRS();
builder.Services.AddStorage(builder.Configuration);

await using var app = builder.Build();

// configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger().UseSwaggerUI();
}

Middlewares(app, app.Environment);

// HTTP verbs
app.MapPost("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] CreateEmployeeDTO model, CancellationToken cancellationToken) =>
{
    var command = new CreateEmployeeCommand(model);
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("CreateEmployee");

app.MapGet("/api/employee", async (IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
{
    var query = new ReadEmployeesQuery();
    var response = await dispatcher.Execute<ReadEmployeesQuery, IEnumerable<EmployeeDTO>>(query, cancellationToken);

    return Results.Ok(response);
}).WithName("ReadEmployees");

app.MapGet("/api/employee/{id}", async (IQueryDispatcher dispatcher, [FromQuery] int id, CancellationToken cancellationToken) =>
{
    var query = new ReadEmployeeByIdQuery(id);
    var response = await dispatcher.Execute<ReadEmployeeByIdQuery, EmployeeDTO>(query, cancellationToken);

    return Results.Ok(response);
}).WithName("ReadEmployeeById");

app.MapPut("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] UpdateEmployeeDTO model, CancellationToken cancellationToken) =>
{
    var command = new UpdateEmployeeCommand(model);
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("UpdateEmployee");

app.MapDelete("/api/employee/{id}", async (ICommandDispatcher dispatcher, [FromQuery] int id, CancellationToken cancellationToken) =>
{
    var command = new DeleteEmployeeCommand(id);
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("DeleteEmployee");

await app.RunAsync();

void Middlewares(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseHttpsRedirection();
    app.ConfigureExceptionHandler(env);
}
