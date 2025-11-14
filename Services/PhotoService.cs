using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using EventCardCopilotApp.Models;

namespace EventCardCopilotApp.Services
{
    public class PhotoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PhotoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Photo>> GetPhotosAsync()
        {
            var client = _httpClientFactory.CreateClient();
            return await client.GetFromJsonAsync<List<Photo>>("https://picsum.photos/v2/list") ?? new List<Photo>();
        }

        public async Task<List<Photo>> GetPhotosAsync(int page = 0, int limit = 5)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                return await client.GetFromJsonAsync<List<Photo>>($"https://picsum.photos/v2/list?page={page}&limit={limit}") ?? new List<Photo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching photos: " + ex.Message);
                return new List<Photo>();
            }
        }
    }
}
