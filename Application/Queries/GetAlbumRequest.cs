using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries
{
    public class GetAlbumRequest : IRequest<IEnumerable<Album>>
    {
    }
}
