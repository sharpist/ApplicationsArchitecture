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
    await app.UseSwagger().UseSwaggerUI().UseDatabaseInitializer();
}

app.UseHttpsRedirection();

// HTTP verbs
app.MapGet("/api/employee", async (IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
{
    var query = new GetEmployeesQuery();
    var employees = await dispatcher.Execute<GetEmployeesQuery, IEnumerable<ReadEmployeeDTO>>(query, cancellationToken);

    return Results.Ok(employees);
}).WithName("GetEmployees");

app.MapGet("/api/employee/{id}", async (IQueryDispatcher dispatcher, [FromQuery] int id, CancellationToken cancellationToken) =>
{
    var query = new GetEmployeeByIdQuery(id);
    var employee = await dispatcher.Execute<GetEmployeeByIdQuery, ReadEmployeeDTO>(query, cancellationToken);

    return Results.Ok(employee);
}).WithName("GetEmployeeById");

app.MapPost("/api/employee", async (ICommandDispatcher dispatcher, [FromBody] CreateOrUpdateEmployeeDTO model) =>
{
    var command = new PostEmployeeCommand(model);
    await dispatcher.Execute(command);

    return Results.Ok();
}).WithName("PostEmployee");

await app.RunAsync();
