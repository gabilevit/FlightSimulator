using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Dal.Data;
using Dal.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Hubs;

namespace Server
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
            services.AddSignalR();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IAirportHub, AirportHub>();
            services.AddTransient<IAirportManager, AirportManager>();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddControllers();
            //services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Default", "{controller=Airport}/{action=NewFlight}");
                endpoints.MapHub<AirportHub>("/airporthub");              
            });
        }
    }
}
