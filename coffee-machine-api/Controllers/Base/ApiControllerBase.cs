using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace coffee_machine_api.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator = null;

        protected ISender Mediator => _mediator ??= (ISender)HttpContext.RequestServices.GetService(typeof(ISender))!;
    }
}

