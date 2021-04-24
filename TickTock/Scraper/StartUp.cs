using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper;
using Scraper.Entities;
using Scraper.Repositories;
using Scraper.Services;
using Scraper.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;

[assembly: FunctionsStartup(typeof(StartUp))]
namespace Scraper
{
    [ExcludeFromCodeCoverage]
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.GetContext().Configuration;

            builder.Services.AddSingleton<IShowsRepository, ShowsRepository>();

            //Can add retry policies and polly to this, https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            //But for this purpose I am leaving the function retry policy as it is automatically a 5 times retry policy
            builder.Services.AddHttpClient<IRestClient, RestClient>(client => 
            {
                var apiUri = configuration.GetSection("ApiUri");
                client.BaseAddress = new Uri(apiUri.Value);
            });

            builder.Services.AddSingleton<IShowsService, ShowsService>();
            builder.Services.AddSingleton(serviceProvider => 
                new PlayScraperContext());

            builder.Services.AddLogging();
        }
    }
}
