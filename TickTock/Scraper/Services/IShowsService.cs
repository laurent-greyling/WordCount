using System.Threading.Tasks;

namespace Scraper.Services
{
    public interface IShowsService
    {
        /// <summary>
        /// Retreive shows from API
        /// </summary>
        Task AddRangeAsync();
    }
}
