using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAppExamn.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IMediator _mediator;
        public PhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int idAlbum, string title)
        {
            var request = new GetPhotoRequest { IdAlbum = idAlbum };
            var model = await _mediator.Send(request);
            ViewBag.titleAlbum = title;
            return View(model);
        }
    }
}
