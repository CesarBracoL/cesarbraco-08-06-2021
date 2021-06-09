using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries
{
    public class GetCommentRequest : IRequest<IEnumerable<Comment>>
    {
        public int IdPhoto { get; set; }
    }
}
