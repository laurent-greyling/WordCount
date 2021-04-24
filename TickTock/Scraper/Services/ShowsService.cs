using Newtonsoft.Json;
using Scraper.Entities;
using Scraper.Repositories;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using System;
using System.Linq;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace Scraper.Services
{
    public class ShowsService : IShowsService
    {
        private readonly IShowsRepository _showsRepository;
        private readonly IRestClient _client;
        private readonly string _serviceBusConnection = Environment.GetEnvironmentVariable("SbConnectionString");
        private readonly string _serviceQueueName = Environment.GetEnvironmentVariable("QueueName");

        public ShowsService(IShowsRepository showsRepository,
            IRestClient client)
        {
            _showsRepository = showsRepository;
            _client = client;
        }

        public async Task AddQueueMessageAsync(List<Shows> shows)
        {
            var queueClient = new QueueClient(_serviceBusConnection, _serviceQueueName);

            foreach (var show in shows)
            {
                var apiUri = $"{Environment.GetEnvironmentVariable("ApiUri")}shows/{show.Id}/cast";
                var body = Encoding.UTF8.GetBytes(apiUri);
                var message = new Message
                {
                    Body = body,
                    ContentType = "text/plain"
                };

                await queueClient.SendAsync(message);
            }
        }

        public async Task AddRangeAsync(List<Shows> shows)
        {
            await _showsRepository.AddRangeAsync(shows);
        }

        public async Task<List<Shows>> GetShowsAsync()
        {
            var result = await _client.RetrieveAsync("shows");
            var shows = JsonConvert.DeserializeObject<List<Shows>>(result);

            return _showsRepository.GetNewShows(shows);
        }
    }
}
