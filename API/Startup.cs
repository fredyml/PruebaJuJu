﻿using API.Filters;
using Business.Entities;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectAPI.API
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
            //Servicios                      
           // services.AddScoped<JujuTestContext, JujuTestContext>();
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPostService, PostService>();


            //Agregar cadena de conexion al contexto
            services.AddDbContext<JujuTestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Development")));


            services.AddMvc(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // ======== CONFIGURACIÓN DE SWAGGER =========
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TestAPI", Version = "v1" });
            });
            services.AddHttpContextAccessor();
            services.AddSession();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ======== CONFIGURACIÓN DE SWAGGER =========
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "TestAPI v1");
            });

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();


            app.UseSession();
            app.UseMvc();
        }
    }
}