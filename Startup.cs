using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAPI.Data;
using Microsoft.EntityFrameworkCore;
using BankingAPI.Data.Repo;
using BankingAPI.Data.Interfaces;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using BankingAPI.Helpers;
using AutoMapper;
using BankingAPI.Extensions;

namespace BankingAPI
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking API", Version = "v1" });
                c.ResolveConflictingActions(apidesc=>apidesc.First()) ;
            });

            IdentityModelEventSource.ShowPII = true;
            services.AddDbContext<BankingAPIDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), op=>op.EnableRetryOnFailure()));
            services.AddControllers().AddNewtonsoftJson();
           
            services.AddCors();
            services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           

            var secretKey = Configuration.GetSection("AppSettings:Key").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BankingAPIDbContext bankingAPIDbContext)
        {
            bankingAPIDbContext.Database.Migrate();
            app.ConfigureExceptionHandler(env);
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Banking API V1");
            });

            app.UseRouting();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
