using System;

namespace Domain.Exceptions
{
    public class AlbumServiceException: Exception
    {
        public AlbumServiceException()
        {
            new AlbumServiceException("Error al consultar el servicio para obtener los datos de album.");
        }

        public AlbumServiceException(string message) : base(message)
        {
        }

        public AlbumServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
