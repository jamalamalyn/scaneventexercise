using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http;

namespace FWScanEvent.TrackerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl;
        private readonly string _eventIdParam = "FromEventId";
        private readonly string _limitParam = "Limit";

        public Worker(IConfiguration configuration, ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _apiUrl = _configuration.GetSection("WebAPIs")["EventScanAPI"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // need to fetch latest eventid
                int eventId = 1;

                HttpClient client = _httpClientFactory.CreateClient();

                string uriString = $"{_apiUrl}/scans/scanevents?{_eventIdParam}={eventId}&{_limitParam}=100";

                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uriString));

                _logger.LogInformation(response.StatusCode.ToString());

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}