using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManage.Data;
using Microsoft.EntityFrameworkCore;
using UserManage.Base.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using UserManage.Web.Common;
using System.IO;
using UserManage.Base;
using Microsoft.Extensions.Logging;
using UserManage.Base.AutoMapper;
using NLog.Extensions.Logging;
using NLog.Web;
using UserManage.Service.Jobs;
using System;

namespace UserManage.Web
{
    public class Startup
    {
        IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            this._env = env;

            var builder = new ConfigurationBuilder()
                      .SetBasePath(env.ContentRootPath)
                      .AddJsonFile(Path.Combine("configs", "appsettings.json"), optional: true, reloadOnChange: true)  // Settings for the application
                      .AddJsonFile(Path.Combine("configs", $"appsettings.{env.EnvironmentName}.json"), optional: true, reloadOnChange: true)
                      .AddEnvironmentVariables();                                              // override settings with environment variables set in compose.   

            //if (env.IsDevelopment())
            //{
            //    // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            //    builder.AddUserSecrets();
            //}

            Configuration = builder.Build();
            Globals.Configuration = Configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //连接数据库
            services.AddEntityFrameworkSqlServer().AddDbContext<EFDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), b => b.UseRowNumberForPaging()); });
            services.AddEntityFrameworkSqlServer().AddDbContext<MallDbContext>(options => { options.UseSqlServer(Configuration["ConnectionStrings:MallSqlServer"],b => b.UseRowNumberForPaging()); });
            services.AddEntityFrameworkSqlServer().AddDbContext<IWMSDbContext>(options => { options.UseSqlServer(Configuration["ConnectionStrings:IWMSSqlServer"], b => b.UseRowNumberForPaging()); });
            services.AddScoped<AutoDistributionDataJob>();
            //services.RegisterAppServices();
            services.AddScoped<Service.Interfaces.IAccountAppService, Service.Implements.AccountAppService>();
            services.AddScoped<Service.Interfaces.ISysLogAppService, Service.Implements.SysLogAppService>();
            services.AddScoped<Service.Interfaces.IDistributionAppService, Service.Implements.DistributionAppService>();
            services.AddScoped<Data.IRepository.IDistributionRepository, Data.Repository.DistributionRepository>();
            services.AddScoped<Data.IRepository.IAccountRepository, Data.Repository.AccountRepository>();
            /* 缓存 */
            services.AddMemoryCache();

            services.AddSession();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddControllersAsServices();
            services.AddTimedJob();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _env.ConfigureNLog(Path.Combine("configs", "nlog.config"));
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            //使用TimedJob
            app.UseTimedJob();

            Globals.ServiceProvider = app.ApplicationServices;
            AceMapper.InitializeMap(); /* 初始化 AutoMapper */

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
