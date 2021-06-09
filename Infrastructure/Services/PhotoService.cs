using Domain.Common;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Utf8Json;

namespace Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly EndPoints _endPoints;
        public PhotoService(IHttpClientFactory httpClientFactory, IOptions<EndPoints> options)
        {
            _clientFactory = httpClientFactory;
            _endPoints = options.Value;
        }
        public async Task<IEnumerable<Photo>> GetByAlbumId(int idAlbum)
        {
            string urlRequest = string.Concat(_endPoints.Photo, "?albumId = ", idAlbum.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, urlRequest);
            var client = _clientFactory.CreateClient("data");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new PhotoServiceException();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<IEnumerable<Photo>>(responseStream);
        }
    }
}
