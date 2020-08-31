using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Models;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DependencyInjection
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);



            //register implementations
            services.AddSingleton<ISingletonService, SingletonService>();
            services.AddScoped<IScopedService, ScopedService>();
            services.AddTransient<ITransientService, TransientService>();










            //register settings
            services.Configure<FactorySettings>(Configuration.GetSection(nameof(FactorySettings)));

            var optionsSettings = services
                .BuildServiceProvider()
                .GetService<IOptions<FactorySettings>>();

            var factorySettings = optionsSettings.Value;


            //register oneSetup
            switch (factorySettings.SourceDocuments)
            {
                case "ElasticSearch":
                    services.AddTransient<IDocumentRepository, DocumentsInElasticSearchRepository>();
                    break;
                case "Folder":
                    services.AddTransient<IDocumentRepository, DocumentsInFolderRepository>();
                    break;
                case "Aws":
                    services.AddTransient<IDocumentRepository, DocumentsInAwsRepository>();
                    break;
                default:
                    services.AddTransient<IDocumentRepository, DocumentsInDatabaseRepository>();
                    break;
            }






            //register func "runtime"

            services.AddTransient<DocumentsInElasticSearchRepository>();
            services.AddTransient<DocumentsInFolderRepository>();
            services.AddTransient<DocumentsInDatabaseRepository>();

            services.AddTransient<Func<FactorySettings, IDocumentRepository>>(serviceProvider => key =>
            {
                switch (key.SourceDocuments)
                {
                    case "ElasticSearch":
                        return serviceProvider.GetService<DocumentsInElasticSearchRepository>();
                    case "Folder":
                        return serviceProvider.GetService<DocumentsInFolderRepository>();
                    default:
                        return serviceProvider.GetService<DocumentsInDatabaseRepository>();
                }
            });
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

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
