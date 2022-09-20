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
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DAL;
using EMSstartbit.TokenAuthentication;
using BAL;

namespace EMSstartbit
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
            //services.AddTransient<ITokenManager, TokenManager>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IloginData, loginData>();
            //services.AddTransient<IpermissionData, permissionData>();
            //services.AddTransient<IroleData, roleData>();
            //services.AddTransient<IuserPermissionData, userPermissionData>();
            //services.AddTransient<IrolePermissionData, rolePermissionData>();
            //services.AddTransient<IemployeeData, employeeData>();
            //services.AddTransient<IdesignationData, designationData>();
            //services.AddTransient<IdepartmentData, departmentData>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IloginData, loginData>();
            services.AddScoped<IpermissionData, permissionData>();
            services.AddScoped<IroleData, roleData>();
            services.AddScoped<IuserPermissionData, userPermissionData>();
            services.AddScoped<IrolePermissionData, rolePermissionData>();
            services.AddScoped<IemployeeData, employeeData>();
            services.AddScoped<IdesignationData, designationData>();
            services.AddScoped<IdepartmentData, departmentData>();
            services.AddScoped<IshiftData, shiftData>();
            services.AddScoped<ItestData, testData>();

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddDbContext<EMSDataContext>(options => { 
                options.UseNpgsql(Configuration.GetConnectionString("TestDb"));
               // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);  
            }, ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
