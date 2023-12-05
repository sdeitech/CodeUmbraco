using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.Common.Hosting;

namespace MyProject
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureUmbracoDefaults()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStaticWebAssets();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
