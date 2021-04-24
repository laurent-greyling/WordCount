using Newtonsoft.Json;
using Scraper.Entities;
using Scraper.Repositories;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper.Services
{
    public class ShowsService : IShowsService
    {
        private readonly IShowsRepository _showsRepository;
        private readonly IRestClient _client;

        public ShowsService(IShowsRepository showsRepository,
            IRestClient client)
        {
            _showsRepository = showsRepository;
            _client = client;
        }

        public async Task AddRangeAsync()
        {
            var result = await _client.RetrieveAsync("shows");
            var shows = JsonConvert.DeserializeObject<List<Shows>>(result);

            await _showsRepository.AddRangeAsync(shows);
        }
    }
}
