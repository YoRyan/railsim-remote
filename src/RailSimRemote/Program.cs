using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RailSimRemote
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
