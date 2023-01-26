using FWScan.EventTrackerService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FWScan.EventTrackerService.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<ScanEvent> ScanEvents { get; set; }

        public DataContext() {}

        public DataContext(DbContextOptions options) : base(options) {}
    }
}
