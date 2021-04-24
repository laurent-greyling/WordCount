using Microsoft.EntityFrameworkCore;
using Moq;
using Scraper.Entities;
using Scraper.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ScraperTests.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ShowsRepositoryTests
    {
        private readonly Mock<PlayScraperContext> _mockedContext;
        private readonly Mock<DbSet<Shows>> _mockedShows;

        private Shows _show;
        private Shows _show1;
        private IQueryable<Shows> _shows;

        private readonly string _showName = "FakeName";
        private readonly string _showName1 = "FakeName1";

        private readonly ShowsRepository _repository;

        public ShowsRepositoryTests()
        {
            SetupData();

            _mockedContext = new Mock<PlayScraperContext>();
            _mockedShows = new Mock<DbSet<Shows>>();

            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.Provider).Returns(_shows.Provider);
            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.Expression).Returns(_shows.Expression);
            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.ElementType).Returns(_shows.ElementType);
            _mockedShows.As<IQueryable<Shows>>().Setup(m => m.GetEnumerator()).Returns(_shows.GetEnumerator());

            _mockedContext.Setup(x => x.Shows).Returns(_mockedShows.Object);
            _mockedContext.Setup(x => x.Shows.AddRangeAsync(_shows, It.IsAny<CancellationToken>())).Returns(Task.FromResult(_mockedShows.Object));
            _mockedContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

            _repository = new ShowsRepository(_mockedContext.Object);
        }

        [Fact]
        public void ShowsRepository_AddRangeAsync_ShowsList_Null_Throws_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddRangeAsync(null));
        }

        [Fact]
        public async Task ShowsRepository_AddRangeAsync_Context_AddRange_Executed()
        {
            await _repository.AddRangeAsync(_shows.ToList());

            _mockedContext.Verify(x => x.Shows.AddRangeAsync(_shows, It.IsAny<CancellationToken>()), Times.AtMostOnce);
        }

        [Fact]
        public async Task ShowsRepository_AddRangeAsync_Context_SaveChanges_Executed()
        {
            await _repository.AddRangeAsync(_shows.ToList());

            _mockedContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtMostOnce);
        }

        private void SetupData()
        {
            _show = new Shows 
            {
                Id = 1,
                Name = _showName
            };

            _show1 = new Shows
            {
                Id = 2,
                Name = _showName1
            };

            _shows = new List<Shows> { _show, _show1 }.AsQueryable();
        }
    }
}
