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
    public class AlbumService : IAlbumService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly EndPoints _endPoints;
        public AlbumService(IHttpClientFactory httpClientFactory, IOptions<EndPoints> options)
        {
            _clientFactory = httpClientFactory;
            _endPoints = options.Value;
        }
        public async Task<IEnumerable<Album>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _endPoints.Album);
            var client = _clientFactory.CreateClient("data");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new AlbumServiceException();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<IEnumerable<Album>>(responseStream);
        }
    }
}
