using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_Assignment.Models;
using DAB3_Assignment.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using DAB3_Assignment.SeedData;

namespace DAB3_Assignment
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Seeding seeduser;
        //public SeedCircle seedcircle;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            seeduser = new Seeding(Configuration);
            //seedcircle = new SeedCircle(Configuration);

        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Seeding>();
            //services.AddScoped<SeedCircle>();

            services.Configure<SocialNetworkDatabaseSettings>(
                Configuration.GetSection(nameof(SocialNetworkDatabaseSettings)));

            services.AddSingleton<ISocialNetworkDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SocialNetworkDatabaseSettings>>().Value);

            services.AddSingleton<UserService>();
            services.AddSingleton<UpdateService>();
            services.AddSingleton<CircleService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
