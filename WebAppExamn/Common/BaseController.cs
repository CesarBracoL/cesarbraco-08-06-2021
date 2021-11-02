using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAppExamn.Common
{
    public class BaseController: Controller
    {
        protected readonly IAppLogger _logger;
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator, IAppLogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}
