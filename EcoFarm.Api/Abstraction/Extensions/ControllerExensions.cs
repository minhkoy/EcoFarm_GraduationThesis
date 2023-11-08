using EcoFarm.Api.Controllers;
using EcoFarm.Application.Common.Results;
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
        public static ActionResult FromResult<T>(this BaseController controller, Result<T> result)
        {
            return result?.ResultType switch
            {
                ResultTypes.Ok => controller.Ok(result),
                ResultTypes.NotFound => controller.NotFound(result),
                ResultTypes.BadRequest => controller.BadRequest(result),
                ResultTypes.Unexpected => controller.BadRequest(result),
                ResultTypes.Unauthorized => controller.Unauthorized(result),
                ResultTypes.Forbidden => controller.Forbid(),
                ResultTypes.InternalServerError => throw new Exception(result.Message),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
