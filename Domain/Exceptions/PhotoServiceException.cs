using System;

namespace Domain.Exceptions
{
    public class PhotoServiceException: Exception
    {
        public PhotoServiceException()
        {
            new AlbumServiceException("Error al consultar el servicio para obtener los datos de photos.");
        }

        public PhotoServiceException(string message) : base(message)
        {
        }

        public PhotoServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
