using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Educative.Domain.Repository;
using Educative.Domain.Services;
using Educative.Infrastructure.Persistence.EFCore;
using Educative.Infrastructure.Persistence.EFCore.Repository;
using Educative.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Educative.Application
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
            services.AddDbContext(Configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //repositories
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITutorialRepository, TutorialRepository>();
            services.AddScoped<IMediaObjectRepository, MediaObjectRepository>();
            //resources services
            services.AddScoped<ICreateCourseService, CreateCourseService>();
            services.AddScoped<IUpdateCourseService, UpdateCourseService>();
            services.AddScoped<ICreateTrackService, CreateTrackService>();
            services.AddScoped<IUpdateTrackService, UpdateTrackService>();
            services.AddScoped<ICreateTutorialService, CreateTutorialService>();
            services.AddScoped<IUpdateTutorialService, UpdateTutorialService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Educative.Application", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Educative.Application v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
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
