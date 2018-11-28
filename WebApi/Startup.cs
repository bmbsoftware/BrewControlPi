using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewControlPi.Service.Core.Services;
using BrewControlPi.Service.Sensors.Services;
using BrewControlPi.Service.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace gpioswitcherwebapi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IPinService, PinService>();
            services.AddScoped<IDisplayService, DisplayService>();
            services.AddScoped<ILcdController, LcdController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
        }
    }
}
