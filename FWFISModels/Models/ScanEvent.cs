using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FWScan.Models
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
        public long EventId { get; set; }

        public long ParcelId { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }

        public string Type { get; set; }

        public string StatusCode { get; set; }

        public Device Device { get; set; }

        public User User { get; set; }

        public ScanEvent()
        {
            Type = string.Empty;
            Device = new Device();
            User = new User();
            StatusCode = string.Empty;
        }
    }
}
