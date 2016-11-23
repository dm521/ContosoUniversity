using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Bootstrapper;
using ContosoUniversity.Dto;
using ContosoUniversity.Application.Interfaces;
using ContosoUniversity.Application.Services;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Repository.Interfaces;
using ContosoUniversity.Repository.Data;

using ContosoUniversity.UnitOfWork.Interfaces;
using ContosoUniversity.UnitOfWork.Persistence;
using AutoMapper;
using ContosoUniversity.Web.Data;

namespace ContosoUniversity.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<EntityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region register application service
            services.AddScoped<IDbContext, EntityContext>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
            services.AddTransient<IEnrollmentService, EnrollmentService>();

            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseService, CourseService>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           
        }
    }
}
