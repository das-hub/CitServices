using System.IO;
using CitServices.PhoneStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CitServices.Extensions
{
    public static class ServiceCollectionEx
    {
        public static void AddPhoneBookService(this IServiceCollection service)
        {
            ServiceProvider provider = service.BuildServiceProvider();

            IHostingEnvironment env = provider.GetService<IHostingEnvironment>();
            IConfiguration conf = provider.GetService<IConfiguration>();
            
            service.AddSingleton<IPhoneBook>(new PhoneBook(Path.Combine(env.WebRootPath, conf["PhoneStorage:file"]), conf["PhoneStorage:sheet"]));
        }
    }
}
