using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FWScan.Models
{
    [Produces("application/json")]
    public class EventResult
    {
        public List<ScanEvent>? ScanEvents { set {  } }

        public EventResult()
        {
            ScanEvents = new List<ScanEvent>();
        }
    }

    [Produces("application/json")]
    public class ScanEvent
    {
        public long EventId { get; set; }

        public long ParcelId { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }

        public string? Type { get; set; }

        public string? StatusCode { get; set; }

        public Device? Device { get; set; }

        public User? User { get; set; }
    }
}
