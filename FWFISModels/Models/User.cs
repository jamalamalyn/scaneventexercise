using Microsoft.AspNetCore.Mvc;

namespace FWScan.Models
{
    [Produces("application/json")]
    public class User
    {
        public string? UserId { get; set; }
        public string? CarrierId { get; set; }
        public string? RunId { get; set; }
    }
}
