using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EcoFarm.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMediator _mediator { get; set; }

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}