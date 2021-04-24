﻿using Microsoft.EntityFrameworkCore;
using Scraper.Entities;
using Scraper.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scraper.Repositories
{
    public class ShowsRepository : IShowsRepository
    {
        private readonly PlayScraperContext _context;

        public ShowsRepository(PlayScraperContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<Shows> shows)
        {
            Ensure.ArgumentNotNull(shows, nameof(shows));

            await _context.Shows.AddRangeAsync(NewShows(shows));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Shows> Retrieve() => _context.Shows.AsNoTracking();

        /// <summary>
        /// Check if list in DB and Api correlate, only take from API where ID does not exist and add these as new shows to DB
        /// </summary>
        /// <param name="shows"></param>
        /// <returns></returns>
        private List<Shows> NewShows(List<Shows> shows)
        {
            var existingShows = Retrieve();

            return shows.Where(x => !existingShows.Any(y => y.Id == x.Id)).ToList();
        }
    }
}
