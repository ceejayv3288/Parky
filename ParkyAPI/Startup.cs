using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkyAPI.Data;
using ParkyAPI.Mappers.ParkyMapper;
using ParkyAPI.Repositories;
using ParkyAPI.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParkyAPI
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();
            services.AddAutoMapper(typeof(ParkyMappings));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ParkyOpenAPISpecNationalParks",
                                    new Microsoft.OpenApi.Models.OpenApiInfo()
                                    {
                                        Title = "Parky API (National Parks)",
                                        Version = "1",
                                        Description = "Parky API National Parks",
                                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                                        {
                                            Email = "ceejayv328@gmail.com",
                                            Name = "Christian Joseph Vargas",
                                            Url = new Uri("https://www.linkedin.com/in/christian-joseph-vargas-0001481a3/")
                                        },
                                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                                        {
                                            Name = "MIT License",
                                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                                        }
                                    }) ;

                options.SwaggerDoc("ParkyOpenAPISpecTrails",
                                    new Microsoft.OpenApi.Models.OpenApiInfo()
                                    {
                                        Title = "Parky API (Trails)",
                                        Version = "1",
                                        Description = "Parky API Trails",
                                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                                        {
                                            Email = "ceejayv328@gmail.com",
                                            Name = "Christian Joseph Vargas",
                                            Url = new Uri("https://www.linkedin.com/in/christian-joseph-vargas-0001481a3/")
                                        },
                                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                                        {
                                            Name = "MIT License",
                                            Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                                        }
                                    });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecNationalParks/swagger.json", "Parky API National Parks");
                options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecTrails/swagger.json", "Parky API Trails");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
