using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Application.Interfaces;
using ContosoUniversity.Application.Services;
using ContosoUniversity.Repository.Interfaces;
using ContosoUniversity.Repository.Data;
using ContosoUniversity.UnitOfWork.Interfaces;
using ContosoUniversity.UnitOfWork.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ContosoUniversity.Dto;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Bootstrapper
{
    public static class Initializer
    {
        public static void Initialize(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EntityContext>(options =>
                    options.UseSqlServer(connectionString));

            ConfigureMapper();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<IDbContext, EntityContext>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
            services.AddTransient<IEnrollmentService, EnrollmentService>();

            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseService, CourseService>();

        }

        public static void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
            });
        }
    }
}
