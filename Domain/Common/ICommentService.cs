using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetByPhotoId(int idPhoto);
    }
}
