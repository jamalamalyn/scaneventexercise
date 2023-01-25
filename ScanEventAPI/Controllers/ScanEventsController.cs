using FWScan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FWScan.EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanEventsController : ControllerBase
    {
        [HttpGet]
        [Route("v1/scan/scanevents")]
        public ActionResult<EventResult> GetScanEvents(long FromEventId = 1, int Limit = 100)
        {

            EventResult result = new EventResult();

            try
            {
                List<ScanEvent> scanEvents = new List<ScanEvent>();

                using (StreamReader r = new StreamReader("events-store.json"))
                {
                    string json = string.Empty;
                    json = r.ReadToEnd();
                    scanEvents = JsonConvert.DeserializeObject<List<ScanEvent>>(json) ?? new List<ScanEvent>();
                }

                if (scanEvents.Count > 0)
                {
                    List<ScanEvent> trimmedEvents = scanEvents.Where(x => x.EventId >= FromEventId).ToList();
                    int take = trimmedEvents.Count >= Limit ? Limit : trimmedEvents.Count;
                    result.ScanEvents = trimmedEvents.GetRange(0, take);
                }
            }
            catch(Exception ex)
            {
                // hello
            }


            return Ok(result);
        }
    }
}
