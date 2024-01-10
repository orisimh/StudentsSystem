using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.RpcContracts.Caching;
using MongoDB.Driver;
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
            // services.adde;
           services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
               new Microsoft.OpenApi.Models.OpenApiInfo
               {
                   Title = "New Swagger",
                   Description = "New Swagger Document",
                   Version = "v1"
               });
            //    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
             //   var filepath = Path.Combine(AppContext.BaseDirectory, filename);
               // options.IncludeXmlComments(filepath);
            });
            services.AddControllers().AddNewtonsoftJson();
            var connectionstring = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DemoDB>(options => options.UseSqlServer(connectionstring));

            //services.AddSession(op => new MongoDBConnectionString());


           var scoped = services.AddSingleton<DBSeeder>(); // x => x.GetRequiredService<DBSeeder>()

            // var dbseed =
            //dbseed.Seed();

            //     return dbseed;



            // scoped.Seed();


            //var dbseer = services.GetRequiredService<DBSeeder>();

            services.Configure<MongoDBConnectionString>(Configuration.GetSection("mongodb"));
            services.AddSingleton<IMongoClient>(e => new MongoClient(Configuration.GetValue<string>("mongodb:connection"))); // , MongoClient
            services.AddSingleton<MongoDBConnectionString>(e => e.GetRequiredService<IOptions<MongoDBConnectionString>>().Value);


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


           // var tokenKey = Configuration.GetValue<string>("TokenKey");
            //var key = Encoding.ASCII.GetBytes(tokenKey);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //        , ValidateLifetime=true 
            //    };
            //});

            //services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(tokenKey));
        
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
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBSeeder>();
                // context.Seed(); ori cancelled //Database.Migrate();
                //context.Database.Migrate();

            }


            // app.UseCors(builder => builder
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .AllowAnyOrigin()
            ////.SetIsOriginAllowed((host) => true)
            ////.AllowCredentials()
            //);
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                    options.DocumentTitle = "My Swagger";

                });
                //app.UseMvc();

            }



    }
}
