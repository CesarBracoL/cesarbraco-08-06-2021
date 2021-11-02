using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebAppExamn.Common;

namespace WebAppExamn.Controllers
{
    public class CommentController : BaseController
    {
        public CommentController(IMediator mediator, IAppLogger logger) : base(mediator, logger)
        {
        }
        public async Task<IActionResult> IndexAsync(int idPhoto)
        {
            var watch = new Stopwatch();
            watch.Start();

            var model = await _mediator.Send(idPhoto);

            watch.Stop();
            _logger.Info("Get Comments of {idPhoto}. {elapsed} ", idPhoto, watch.Elapsed.TotalSeconds);

            return View(model);
        }
    }
}
