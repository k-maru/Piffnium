using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Piffnium.Comparator.Abstraction;
using Piffnium.Comparator.ImageSharp;
using Piffnium.Repository.Abstraction;
using Piffnium.Repository.FileSystem;

namespace Piffnium.Api
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
            services.AddMvc();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
