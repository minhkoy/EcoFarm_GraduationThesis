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
                //ProblemDetails problemDetails = null;
                //Application.Common.Results.Result<object> result = null;
                if (e is ValidationException validationException)
                {
                    //problemDetails = new ProblemDetails(
                    //    Status: StatusCodes.Status400BadRequest,
                    //    Type: "ValidationError",
                    //    Title: "Lỗi kiểm tra dữ liệu đầu vào",
                    //    Detail: "Thông tin không hợp lệ, vui lòng kiểm tra lại",
                    //    Errors: validationException.Errors
                    //);
                    List<ValidationError> errors = new();
                    foreach (var error in validationException.Errors)
                    {
                        errors.Add(new ValidationError(error.PropertyName, error.ErrorMessage, error.ErrorCode, ValidationSeverity.Error));
                    }
                    await context.Response
                        .WriteAsJsonAsync(Result.Invalid(errors));
                    //result = new BadRequestResult<object>(validationException.Message, validationException.Errors);
                }
                else if (e is SecurityTokenException)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //await context.Response.WriteAsJsonAsync(Result.Unauthorized());
                }
                else 
                {
                    //problemDetails = new ProblemDetails(
                    //    Status: StatusCodes.Status500InternalServerError,
                    //    Type: "ServerError",
                    //    Title: "Server Error",
                    //    Detail: $"An unexpected error has occured: {e}",
                    //    null
                    //);
                    _logger.LogError($"{e}");
                    await context.Response
                        .WriteAsJsonAsync(Result.CriticalError(e.Message, e.StackTrace));
                    //result = new UnexpectedResult<object>();
                }

                //context.Response.StatusCode = problemDetails.Status;
                //await context.Response.WriteAsJsonAsync(result);
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
