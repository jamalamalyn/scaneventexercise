using FWScan.EventTrackerService.Data;
using FWScan.EventTrackerService.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FWScan.EventTrackerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private readonly string _eventIdParam = "FromEventId";
        private readonly string _limitParam = "Limit";
        private long _latestEventId;
        private readonly long _take = 100;
        private DataAccess _db;
        private HttpClient _httpClient;

        public Worker(IConfiguration configuration, ILogger<Worker> logger, IHttpClientFactory httpClientFactory, DataContext dataContext)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();

            _configuration = configuration;
            _apiUrl = _configuration.GetSection("WebAPIs")["EventScanAPI"];
            _db  = new DataAccess(dataContext);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _latestEventId = _db.GetLatestScanEventId();
                string uriString = $"{_apiUrl}/scans/scanevents?{_eventIdParam}={_latestEventId}&{_limitParam}={_take}";

                try
                {
                    var response = await _httpClient.SendAsync(
                        new HttpRequestMessage(HttpMethod.Get, uriString)
                    );

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (response.Content.ReadAsStringAsync().Result != null)
                        {
                            EventResult eventResult = new EventResult();

                            try
                            {
                                string json = string.Empty;
                                json = response.Content.ReadAsStringAsync().Result ?? "";

                                eventResult = JsonConvert.DeserializeObject<EventResult>(json);

                                if (eventResult?.ScanEvents.Count > 0)
                                {
                                    _db.AddScanEvents(eventResult.ScanEvents);
                                    _logger.LogInformation($"{eventResult.ScanEvents.Count} scan events have been recorded.");
                                }
                            }
                            catch(Exception ex)
                            {
                                _logger.LogWarning("Bad JSON data", ex.Message);
                            }
                        }
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            _logger.LogInformation($"No new scan events");
                        }
                        else if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            _logger.LogError(
                                $"Internal server error at ${uriString}", response.StatusCode
                            );
                        }
                        else
                        {
                            _logger.LogInformation($"{response.StatusCode}");
                        }
                    }
                }
                catch(Exception ex) 
                {
                    _logger.LogError($"Service at {uriString} is non responsive: {ex.Message}");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}