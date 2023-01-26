using FWScan.EventTrackerService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FWScan.EventAPI.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class ScanEventsController : ControllerBase
    {
        [HttpGet]
        [Route("scans/scanevents")]
        public ActionResult<ActionResult<EventResult>> GetScanEvents(long FromEventId = 0, int Limit = 100)
        {
            try
            {
                EventResult result = new EventResult();
                List<ScanEvent> scanEvents = new List<ScanEvent>();

                using (StreamReader r = new StreamReader("events-store.json"))
                {
                    string json = string.Empty;
                    json = r.ReadToEnd();
                    scanEvents = JsonConvert.DeserializeObject<List<ScanEvent>>(json) ?? new List<ScanEvent>();
                }

                List<ScanEvent> trimmedEvents = scanEvents.Where(x => x.EventId > FromEventId).ToList();
                int take = trimmedEvents.Count >= Limit ? Limit : trimmedEvents.Count;

                if (trimmedEvents.Count > 0)
                {
                    result.ScanEvents = trimmedEvents.GetRange(0, take);

                    return Ok(result);
                }
                else 
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
