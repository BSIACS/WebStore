﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.Services;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using WebStore.Data;
using WebStore.Infrastructure.Services.InSqlDataBase;
using WebStore.Employees.DAL;
using WebStore.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.Infrastructure.Services.InCookies;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));        //Соединение с базой данных WebStoreDb
            services.AddTransient<WebStoreDbInitializer>();                             // Добавлен инициализатор базы данных WebStoreDb

            services.AddDbContext<EmployeesDb>(opt => opt.UseSqlServer(_configuration.GetConnectionString("EmployeesDbConnection")));   //Соединение с базой данных EmployeesDb
            services.AddTransient<EmployeesDbInitializer>();                            // Добавлен инициализатор базы данных EmployeesDb

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt => {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt => {
                opt.Cookie.Name = "WebStore.GB";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services.AddTransient<IEmployeesDataService, InSqlDbEmployeesData>();      // Добавлен сервис для работы со списком сотрудников
            services.AddTransient<IProductData, InSqlDbProductData>();
            services.AddScoped<ICartService, InCookiesCartService>();
            services.AddTransient<IOrderService, SqlOrderService>();
            services.AddMvc();                                                          // Добавлены сервисы MVC
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer webstoreDbInitializer, EmployeesDbInitializer employeesDbInitializer)
        {
            webstoreDbInitializer.Initialize();                                         // Старт инициализатора базы данных WebStoreDb
            employeesDbInitializer.Initialize();                                        // Старт инициализатора базы данных EmployeesDb

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();                                                   // Добавлена связь с браузером
            }

            app.UseRouting();                                                           // Подключение EndpointRoutingMiddleware

            app.UseStaticFiles();                                                       // Включена поддержка статических файлов

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(                                           // Определение маршрутов
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
