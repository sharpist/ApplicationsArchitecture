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

app.UseHttpsRedirection();
Middlewares(app, app.Environment);

// HTTP verbs
app.MapPost("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] CreateEmployeeDTO model, CancellationToken cancellationToken) =>
{
    var command = new PostEmployeeCommand(model);
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("PostEmployee");

app.MapGet("/api/employee", async (IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
{
    var query = new GetEmployeesQuery();
    var response = await dispatcher.Execute<GetEmployeesQuery, IEnumerable<ReadEmployeeDTO>>(query, cancellationToken);

    return Results.Ok(response);
}).WithName("GetEmployees");

app.MapGet("/api/employee/{id}", async (IQueryDispatcher dispatcher, [FromQuery] int id, CancellationToken cancellationToken) =>
{
    var query = new GetEmployeeByIdQuery(id);
    var response = await dispatcher.Execute<GetEmployeeByIdQuery, ReadEmployeeDTO>(query, cancellationToken);

    return Results.Ok(response);
}).WithName("GetEmployeeById");

app.MapPut("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] UpdateEmployeeDTO model, CancellationToken cancellationToken) =>
{
    var command = new PutEmployeeCommand(model);
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("PutEmployee");

app.MapDelete("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] DeleteEmployeeDTO model, CancellationToken cancellationToken) =>
{
    var command = new DeleteEmployeeCommand(model);
    await dispatcher.Execute(command, cancellationToken);

    return Results.Ok();
}).WithName("DeleteEmployee");

await app.RunAsync();

void Middlewares(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.ConfigureExceptionHandler(env);
}
