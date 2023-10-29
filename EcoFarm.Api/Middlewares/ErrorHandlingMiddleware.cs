using FluentValidation;
using System.Text.Json;

namespace EcoFarm.Api.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                ProblemDetails problemDetails = null;
                if (e is ValidationException validationException)
                {
                    problemDetails = new ProblemDetails(
                        Status: StatusCodes.Status400BadRequest,
                        Type: "ValidationError",
                        Title: "Validation error",
                        Detail: "One or more validation errors has occured",
                        Errors: validationException.Errors
                    );
                }
                else
                {
                    problemDetails = new ProblemDetails(
                        Status: StatusCodes.Status500InternalServerError,
                        Type: "ServerError",
                        Title: "Server Error",
                        Detail: $"An unexpected error has occured: {e}",
                        null
                    );
                }

                context.Response.StatusCode = problemDetails.Status;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        internal record ProblemDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object> Errors);
    }
}
