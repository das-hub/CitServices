using CitServices.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CitServices
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddPhoneBookService();            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "Phones/{department}", 
                    new {controller = "Phone", action = "List" });

                routes.MapRoute(
                    "",
                    "{controller=Phone}/{action=List}");

                routes.MapRoute(
                    "",
                    "{controller}/{action}");
            });


        }
    }
}
