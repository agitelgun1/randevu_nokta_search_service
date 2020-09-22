using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RandevuNokta.Search.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:12070/")
                .UseKestrel()
                .UseStartup<Startup>();
    }
}
