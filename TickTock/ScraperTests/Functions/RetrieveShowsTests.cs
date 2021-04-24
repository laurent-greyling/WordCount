using Moq;
using Scraper;
using Scraper.Services;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.Azure.WebJobs;

namespace ScraperTests.Functions
{
    [ExcludeFromCodeCoverage]
    public class RetrieveShowsTests
    {
        private readonly Mock<IShowsService> _mockedShowsService;

        private readonly RetrieveShows _retrieveShows;

        public RetrieveShowsTests()
        {
            _mockedShowsService = new Mock<IShowsService>();

            _mockedShowsService.Setup(x => x.AddRangeAsync()).Verifiable();

            _retrieveShows = new RetrieveShows(_mockedShowsService.Object);
        }

        [Fact]
        public async Task RetrieveShows_Run_Executed()
        {
            await _retrieveShows.Run(It.IsAny<TimerInfo>(), It.IsAny<ILogger>());

            _mockedShowsService.Verify(x => x.AddRangeAsync(), Times.Once);
        }
    }
}
