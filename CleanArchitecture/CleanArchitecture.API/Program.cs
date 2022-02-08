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
app.MapPost("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] PostEmployeeCommand command, CancellationToken cancellationToken) =>
{
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("PostEmployee");

app.MapGet("/api/employee", async (IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
{
    var query = new GetEmployeesQuery();
    var response = await dispatcher.Execute<GetEmployeesQuery, IEnumerable<Employee>>(query, cancellationToken);

    return Results.Ok(response);
}).WithName("GetEmployees");

app.MapGet("/api/employee/{id}", async (IQueryDispatcher dispatcher, [FromQuery] int id, CancellationToken cancellationToken) =>
{
    var query = new GetEmployeeByIdQuery(id);
    var response = await dispatcher.Execute<GetEmployeeByIdQuery, Employee>(query, cancellationToken);

    return Results.Ok(response);
}).WithName("GetEmployeeById");

app.MapPut("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] PutEmployeeCommand command, CancellationToken cancellationToken) =>
{
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("PutEmployee");

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
