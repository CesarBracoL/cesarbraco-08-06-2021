using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IAlbumService
    {
        Task<IEnumerable<Album>> GetAll();
    }
}
