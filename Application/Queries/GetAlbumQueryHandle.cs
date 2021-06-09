using Domain.Common;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAlbumQueryHandle : IRequestHandler<GetAlbumRequest, IEnumerable<Album>>
    {
        private readonly IAlbumService _albumService;
        public GetAlbumQueryHandle(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        public async Task<IEnumerable<Album>> Handle(GetAlbumRequest request, CancellationToken cancellationToken)
        {
            var albums = await _albumService.GetAll();
            return albums;
        }
    }
}
