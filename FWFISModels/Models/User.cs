using Microsoft.AspNetCore.Mvc;

namespace FWScan.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string CarrierId { get; set; }
        public string RunId { get; set; }

        public User()
        {
            UserId = string.Empty;
            CarrierId = string.Empty;
            RunId = string.Empty;
        }
    }
}
