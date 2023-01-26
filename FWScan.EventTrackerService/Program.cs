using FWScan.EventTrackerService;
using FWScan.EventTrackerService.Data;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostingContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddSingleton<DataAccess>();
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlite(hostingContext.Configuration.GetConnectionString("DefaultConnection")),
                    ServiceLifetime.Singleton);
                services.AddHttpClient();
            })
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configurationRoot = configuration.Build();
            })
            .Build();
        // On-launch do a migration to make sure schema is on latest version
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataContext>();
            context.Database.Migrate();
        }

        await host.RunAsync();
    }
}