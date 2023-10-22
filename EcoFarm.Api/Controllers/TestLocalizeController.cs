//using EcoFarm.Application.Interfaces.Localization;

using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Localization;
using EcoFarm.Application.Localization.Services;
using EcoFarm.Domain.Common.Values.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace EcoFarm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestLocalizeController : BaseController
    {
        private readonly ILocalizeService _localizeService;
        private static string lang = "vi";

        public TestLocalizeController(IMediator mediator, ILocalizeService localizeService) : base(mediator)
        {
            _localizeService = localizeService;
            //HttpContext.Request.Headers["Accept-Language"] = "en";
        }

        [HttpPost("[action]")]
        public IActionResult ChangeLanguage([FromBody] string cultureNew)
        {
            CultureInfo culture;
            if (Thread.CurrentThread.CurrentCulture.Name == "vi")
            {
                culture = CultureInfo.CreateSpecificCulture(cultureNew);
            }
            else
            {
                culture = CultureInfo.CreateSpecificCulture("vi");
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            var headers = HttpContext.Request.Headers;
            var uno = _localizeService
                          .GetMessage(LocalizationEnum.MissingRequiredFields)
                      ?? string.Empty;
            return Ok(uno);
        }

        //public IActionResult 
    }
}