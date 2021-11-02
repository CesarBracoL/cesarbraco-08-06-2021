using Application.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebAppExamn.Common;

namespace WebAppExamn.Controllers
{
    public class PhotoController : BaseController
    {
        public PhotoController(IMediator mediator, IAppLogger logger) : base(mediator, logger)
        {
        }

        public async Task<IActionResult> Index(int idAlbum, string title)
        {
            var watch = new Stopwatch();
            watch.Start();

            var request = new GetPhotoRequest { IdAlbum = idAlbum };
            var model = await _mediator.Send(request);
            ViewBag.titleAlbum = title;

            watch.Stop();
            _logger.Info("Get Photos of Album. {idAlbum} {elapsed} ", idAlbum, watch.Elapsed.TotalSeconds);

            return View(model);
        }
    }
}
