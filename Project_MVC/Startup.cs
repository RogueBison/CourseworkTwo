using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Project_MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

        // Routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Order",
                    pattern: "Albums/Order",
                    defaults: new { controller = "Albums", action = "Order" });
                endpoints.MapControllerRoute(
                    name: "Order",
                    pattern: "Tracks/Order",
                    defaults: new { controller = "Tracks", action = "Order" });
                endpoints.MapControllerRoute(
                    name: "insertPage",
                    pattern: "Albums/InsertPage",
                    defaults: new { controller = "Albums", action = "InsertPage" });
                endpoints.MapControllerRoute(
                    name: "Insert",
                    pattern: "Albums/Insert/{Title}/{ArtistId}",
                    defaults: new { controller = "Albums", action = "Insert" });
                endpoints.MapControllerRoute(
                    name: "Search",
                    pattern: "Albums/Search/{searchText}",
                    defaults: new { controller = "Albums", action = "Search" });
                endpoints.MapControllerRoute(
                    name: "deletePage",
                    pattern: "Albums/deletePage/{id}",
                    defaults: new { controller = "Albums", action = "deletePage" });
                endpoints.MapControllerRoute(
                    name: "Delete",
                    pattern: "Albums/Delete/{id}",
                    defaults: new { controller = "Albums", action = "Delete" });
                endpoints.MapControllerRoute(
                    name: "updateForm",
                    pattern: "Albums/updateForm/{id}",
                    defaults: new { controller = "Albums", action = "updateForm" });
                endpoints.MapControllerRoute(
                    name: "Update",
                    pattern: "Albums/Update/{id}/{Title}/{ArtistName}/{TrackNames}/{TrackIds}/{NewTrackName}",
                    defaults: new { controller = "Albums", action = "Update" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
