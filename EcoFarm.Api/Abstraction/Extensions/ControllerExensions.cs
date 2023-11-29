using EcoFarm.Api.Controllers;
//using EcoFarm.Application.Common.Results;
using Ardalis.Result;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Api.Abstraction.Extensions
{
    public static class ControllerExensions
    {
        public static ActionResult FromResult<T>(this BaseController controller, Result<T> result, ILogger logger)
        {
            if (result.IsSuccess)
            {
                var message = string.Empty;
                if (!string.IsNullOrEmpty(result.SuccessMessage))
                {
                    message = result.SuccessMessage;
                }
                else
                {
                    message = "Xử lý thành công";
                }
                logger.LogInformation(message);
                if (typeof(T) == typeof(bool))
                {
                    return controller.NoContent();
                }
                switch (controller.Request.Method)
                {
                    case nameof(HttpMethod.Post):
                        return controller.CreatedAtAction(string.Empty, result);
                    default:
                        return controller.Ok(result);
                }
            }
            if (result.Errors is not null && result.Errors.Count() > 0)
            {
                logger.LogError(result.Errors.First());
            }
            return result?.Status switch
            {
                ResultStatus.Ok => controller.Ok(result),
                ResultStatus.NotFound => controller.NotFound(result),
                ResultStatus.Invalid => controller.BadRequest(result),
                ResultStatus.Error => controller.BadRequest(result),
                ResultStatus.Conflict => controller.Conflict(result),
                //ResultStatus. => controller.BadRequest(result),
                ResultStatus.Unauthorized => controller.Unauthorized(result),
                ResultStatus.Forbidden => controller.Forbid(),
                ResultStatus.CriticalError => throw new Exception("Đã có lỗi xảy ra. Vui lòng thử lại sau"),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
