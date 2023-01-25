using FWScan.Models;
using Microsoft.EntityFrameworkCore;

namespace FWScan.EventTrackerService.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        DbSet<ScanEvent> ScanEvents { get; set; }

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
