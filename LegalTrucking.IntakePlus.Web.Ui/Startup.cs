using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using LegalTrucking.IntakePlus.Web.Ui.Membership;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CosmosDBRepository;

namespace LegalTrucking.IntakePlus.Web.Ui
{
    public class Startup
    {
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<Persistence>((s) =>
            {
                var p = new Persistence(
                    new Uri(Configuration["CosmosDB:URL"]),
                            Configuration["CosmosDB:PrimaryKey"],
                            Configuration["CosmosDB:DatabaseId"]);
                p.EnsureSetupAsync().Wait();
                return p;
            });

            services.AddCustomMembership<CosmosDBMembership>((options) => {
                options.AuthenticationType = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultPathAfterLogin = "/Company/Index";
                options.DefaultPathAfterLogout = "/Account/Login";
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie((options) =>
             {
                 options.LoginPath = new PathString("/account/login");
                 options.Events = new CookieAuthenticationEvents()
                 {
                   
                     OnValidatePrincipal = async (c) =>
                     {
                         var membership = c.HttpContext.RequestServices.GetRequiredService<ICustomMembership>();
                         var isValid = await membership.ValidateLoginAsync(c.Principal);
                         if (!isValid)
                         {
                             c.RejectPrincipal();
                         }
                     }
                 };
             });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc();
            services.AddOptions();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
