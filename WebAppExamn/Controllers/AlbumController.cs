using Application.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebAppExamn.Common;

namespace WebAppExamn.Controllers
{
    public class AlbumController : BaseController
    {
        public AlbumController(IMediator mediator, IAppLogger logger) : base(mediator, logger)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            var watch = new Stopwatch();
            watch.Start();

            var model = await _mediator.Send(new GetAlbumRequest());

            watch.Stop();            
            _logger.Info("Get Album request. {elapsed} ", watch.Elapsed.TotalSeconds);

            return View(model);
        }
    }
}
