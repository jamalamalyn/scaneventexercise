using FWScan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Net;
using System.Reflection;

namespace FWScan.EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanEventsController : ControllerBase
    {
        [HttpGet]
        [Route("v1/scan/scanevents")]
        public IActionResult GetScanEvents(long FromEventId, int limit)
        {
            ScanEvent scanEvent = new ScanEvent
            {
                EventId = 83269,
                ParcelId = 5002,
                Type = "PICKUP",
                CreatedDateTimeUtc = DateTime.UtcNow,
                StatusCode = "",
                Device = new Device
                {
                    DeviceTransactionId = 83269,
                    DeviceId = 103,
                },
                User = new User
                {
                    UserId = "NC1001",
                    CarrierId = "NC",
                    RunId = "100"
                }
            };

            EventResult result = new EventResult()
            {
                ScanEvents = new List<ScanEvent>
                {
                    scanEvent
                }
            };

            return new OkObjectResult(result);
        }
    }
}
