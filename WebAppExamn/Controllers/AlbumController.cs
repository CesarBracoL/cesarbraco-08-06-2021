using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAppExamn.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IMediator _mediator;
        public AlbumController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _mediator.Send(new GetAlbumRequest());
            return View(model);
        }
    }
}
