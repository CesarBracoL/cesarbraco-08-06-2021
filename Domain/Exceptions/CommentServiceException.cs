using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CommentServiceException: Exception
    {
        public CommentServiceException()
        {
            new CommentServiceException("Error al consultar el servicio para obtener los datos de comments.");
        }

        public CommentServiceException(string message) : base(message)
        {
        }

        public CommentServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
