using Moq;
using Scraper.Entities;
using Scraper.Repositories;
using Scraper.Services;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace ScraperTests.Services
{
    [ExcludeFromCodeCoverage]
    public class ShowsServiceTests
    {
        private readonly Mock<IShowsRepository> _mockedRepository;
        private readonly Mock<IRestClient> _mockedClient;

        private readonly string _fakeApiUri = "shows";
        private readonly string _fakeData = @"[
    {
        'id': 1,
        'name': 'Game of Thrones'
    },
    {
        'id': 4,
        'name': 'Big Bang Theory',
    }
]";

        private readonly ShowsService _showService;

        public ShowsServiceTests()
        {
            _mockedRepository = new Mock<IShowsRepository>();
            _mockedClient = new Mock<IRestClient>();

            _mockedClient.Setup(x => x.RetrieveAsync(_fakeApiUri)).ReturnsAsync(_fakeData).Verifiable();
            _mockedRepository.Setup(x => x.AddRangeAsync(It.IsAny<List<Shows>>())).Verifiable();

            _showService = new ShowsService(_mockedRepository.Object, _mockedClient.Object);
        }

        [Fact]
        public async Task ShowsService_RetrieveAsync_HttpClient_GetAsync_Returns()
        {
            await _showService.AddRangeAsync();

            _mockedClient.Verify(x => x.RetrieveAsync(_fakeApiUri), Times.Once);
        }

        [Fact]
        public async Task ShowsService_RetrieveAsync_Repository_AddRange_Executed()
        {
            await _showService.AddRangeAsync();

            _mockedRepository.Verify(x => x.AddRangeAsync(It.IsAny<List<Shows>>()), Times.Once);
        }
    }
}
