using Domain.Authentication;
using GrpcService.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Data;
using Repository.IRepositories;
using Repository.Mapping;
using Repository.Repositories;
using Repository.UnitOfWork;
using Service.IServices;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddDbContext<ApplicationDbContext>(options
                => options.UseCosmos(
                    "https://oneeleaksdb.documents.azure.com:443/",
                    "mq44w6HocomOqReFwJ1mpRFS1yY4iu1Y4BUiAt383Ae9uhYiLpOYJS8tVEwKf74oy3nYDybHbcjbjXCYc0UH2Q==",
                    databaseName: "OneeLeaks"));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IAuthentificationRepository, AuthentificationRepository>();
            services.AddScoped<IAuthentificationService, AuthentificationService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MappingProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapGrpcService<DeclarationsServices>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
