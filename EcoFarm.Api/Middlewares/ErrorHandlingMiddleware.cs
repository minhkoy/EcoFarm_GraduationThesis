using Ardalis.Result;
using EcoFarm.Application.Common.Results;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
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
                if (e is ValidationException validationException)
                {
                    List<ValidationError> errors = new();
                    foreach (var error in validationException.Errors)
                    {
                        errors.Add(new ValidationError(error.PropertyName, error.ErrorMessage, error.ErrorCode, ValidationSeverity.Error));
                    }
                    await context.Response
                        .WriteAsJsonAsync(Result.Invalid(errors));
                }
                else if (e is SecurityTokenException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                else 
                {

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    _logger.LogError($"{e}");
                }
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
