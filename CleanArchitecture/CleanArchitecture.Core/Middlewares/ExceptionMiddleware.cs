namespace CleanArchitecture.Core.Middlewares;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (contextFeature is not null)
                {
                    var ex = contextFeature.Error;
                    var isDev = env.IsDevelopment();

                    await context.Response.WriteAsync(JsonSerializer.Serialize<ProblemDetails>(
                        new ProblemDetails
                        {
                            Type = ex.GetType().Name,
                            Status = (int)HttpStatusCode.InternalServerError,
                            Instance = contextFeature.Path,
                            Title = isDev ? $"{ex.Message}" : "An error occurred.",
                            Detail = isDev ? ex.StackTrace : null
                        })).ConfigureAwait(false);
                }
            });
        });
    }
}
