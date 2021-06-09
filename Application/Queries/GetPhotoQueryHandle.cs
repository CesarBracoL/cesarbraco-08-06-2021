using Domain.Common;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetPhotoQueryHandle : IRequestHandler<GetPhotoRequest, IEnumerable<Photo>>
    {
        private readonly IPhotoService _photoService;
        public GetPhotoQueryHandle(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        public async Task<IEnumerable<Photo>> Handle(GetPhotoRequest request, CancellationToken cancellationToken)
        {
            var photos = await _photoService.GetByAlbumId(request.IdAlbum);
            return photos;
        }
    }
}
