using Microsoft.AspNetCore.Mvc;

namespace FWScan.Models
{
    [Produces("application/json")]
    public class Device
    {
        public long DeviceId { get; set; }
        public long DeviceTransactionId { get; set; }
    }
}
