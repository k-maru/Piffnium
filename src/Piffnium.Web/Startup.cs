using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Piffnium.Comparator.Abstraction;
using Piffnium.Comparator.ImageSharp;
using Piffnium.Repository.Abstraction;
using Piffnium.Repository.FileSystem;
using Piffnium.Web.Application;
using Piffnium.Web.Infrastructure.Configuration;
using Piffnium.Web.Infrastructure.EntityFramework;

namespace Piffnium.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            var dbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();
            dbContext.Database.Migrate();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;

            services.AddScoped(typeof(ISessionService), typeof(SessionService));
            var repoDirectory = Path.Combine(this.Environment.ContentRootPath, "..\\repository");
            if (!Directory.Exists(repoDirectory))
            {
                Directory.CreateDirectory(repoDirectory);
            }
            services.AddSingleton<IPiffniumRepositoryFactory>(
                new FsPiffeniumRepositoryFactory(new FsRepositoryOptions()
                {
                    RootDirectory = repoDirectory
                })
            );
            services.AddSingleton<IPictureComparatorFactory>(sv =>
            {
                return new PixelComparatorFactory();
            });

            services.Configure<PiffniumSettings>(this.Configuration.GetSection(nameof(PiffniumSettings)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHsts();
            app.UseHttpsRedirection();

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
