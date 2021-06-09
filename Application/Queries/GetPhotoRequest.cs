using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries
{
    public class GetPhotoRequest: IRequest<IEnumerable<Photo>>
    {
        public int IdAlbum { get; set; }
    }
}
