using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoFarm.Api.Controllers.Administration
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceManagerController : BaseController
    {
        public ServiceManagerController(IMediator mediator) : base(mediator)
        {
        }


    }
    
}
