using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppExamn.Controllers
{
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index(int idPhoto)
        {
            var model = await _mediator.Send();
            return View(model);
        }
    }
}
