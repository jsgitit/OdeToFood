using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;
using System;

namespace OdeToFood
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
            services.AddDbContextPool<OdeToFoodDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServiceOnDockerConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        // Enable retry on failure for transient errors
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,  // The maximum number of retries
                            maxRetryDelay: TimeSpan.FromSeconds(5),  // Delay between retries
                            errorNumbersToAdd: null);  // You can specify additional SQL error numbers to retry on
                    });
            });


            // InMemory Database implementation for DEV
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            // A Scoped SQL Server Database Implementation, per http request, for Production
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
