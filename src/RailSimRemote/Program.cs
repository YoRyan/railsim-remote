using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RailSimRemote
{
    class Program
    {
        /// <param name="urls">The URL(s) the web server will listen on.</param>
        static void Main(string urls = "http://localhost:8888")
        {
            var host = WebHost.CreateDefaultBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls(urls)
                .Build();
            host.Run();
        }
    }
}
