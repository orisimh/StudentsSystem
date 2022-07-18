using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.RpcContracts.Caching;
using StackExchange.Redis;
using WebApplication2.Data;
using WebApplication2.Services;

namespace WebApplication2
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
            services.AddControllers().AddNewtonsoftJson();
            var connectionstring = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DemoDB>(options => options.UseSqlServer(connectionstring));
            //services.AddCors(); // Make sure you call this previous to AddMvc
            services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy",
                      builder =>
                      {
                          builder.WithOrigins("http://127.0.0.1:5500", "https://localhost:44383")//.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });
            //services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            //{
            //    builder.WithOrigins("http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
            //}));
            ////services.AddCors();
            services.AddMvc(options=> options.EnableEndpointRouting = false);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis")));
             
            
            //services.AddSingleton<ICacheService, RedisCacheService>();
           // services.AddSingleton<DistributedCacheExtensions>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379,ssl=False";
                //Configuration.GetConnectionString("Redis");
                //options.ConfigurationOptions.
                //options.InstanceName = "RedisDemo_";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("ApiCorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           // app.UseCors(builder => builder
           //    .AllowAnyHeader()
           //    .AllowAnyMethod()
           //    .AllowAnyOrigin()
           ////.SetIsOriginAllowed((host) => true)
           ////.AllowCredentials()
           //);
            
            //app.UseMvc();

        }



    }
}
