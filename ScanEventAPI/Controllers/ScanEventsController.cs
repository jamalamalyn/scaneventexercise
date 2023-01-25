using FWScan.Models;
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
        public ActionResult<EventResult> GetScanEvents(long FromEventId = 1, int Limit = 100)
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

                if (scanEvents.Count > 0)
                {
                    List<ScanEvent> trimmedEvents = scanEvents.Where(x => x.EventId >= FromEventId).ToList();
                    int take = trimmedEvents.Count >= Limit ? Limit : trimmedEvents.Count;
                    result.ScanEvents = trimmedEvents.GetRange(0, take);

                    return Ok(result);
                }
                else 
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
