using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Extensions;
using logistics_BE.Repository;
using logistics_BE.Services.Implementation;
using logistics_BE.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Design;
using logistics_BE.Services;

namespace logistics_BE
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
            services.AddCors();
            services.AddDatabase(Configuration);
            services.AddCaching(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "logistics_BE", Version = "v1" });
            });

            services.AddScoped<IRoadRepo, RoadRepo>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<ILogisticsCenterRepo, LogisticsCenterRepo>();


            services.AddScoped<IRoadService, RoadService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ILogisticsService, LogisticsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "logistics_BE v1"));

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
