using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FWScan.EventTrackerService.Models
{
    public class EventResult
    {
        public List<ScanEvent> ScanEvents { get; set; }

        public EventResult()
        {
            ScanEvents = new List<ScanEvent>();
        }
    }

    public class ScanEvent
    {
        [Key]
        public long EventId { get; set; }

        public long ParcelId { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }

        public string Type { get; set; }

        public string StatusCode { get; set; }

        public ScanEvent()
        {
            Type = string.Empty;
            StatusCode = string.Empty;
        }
    }
}
