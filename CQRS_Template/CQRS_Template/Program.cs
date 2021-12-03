var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore(builder);

await using var app = builder.Build();

// configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.UseDbInitializerAsync();
}

app.UseHttpsRedirection();

// HTTP verbs
app.MapGet("/api/employee", async (EmployeeDbContext context) =>
{
    var employees = await context.Employees.ToArrayAsync();
    return Results.Ok(employees);
}).WithName("GetEmployees");

app.MapGet("/api/employee/{id}", async (EmployeeDbContext context, int id) =>
{
    return await context.Employees.FindAsync(id);
}).WithName("GetEmployeeById");

app.MapPost("/api/employee", async (EmployeeDbContext context, EmployeeModel model) =>
{
    context.Employees.Add(model);
    await context.SaveChangesAsync();
    return Results.Ok();
}).WithName("PostEmployee");

await app.RunAsync();
