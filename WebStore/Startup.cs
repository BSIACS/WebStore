﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Services;

namespace WebStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeesDataService, InMemoryEmployeesData>();      // Добавлен сервис для работы со списком сотрудников
            services.AddMvc();                                                          // Добавлены сервисы MVC
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();                                                   // Добавлена связь с браузером
            }

            app.UseRouting();                                                           // Подключение EndpointRoutingMiddleware

            app.UseStaticFiles();                                                       // Включена поддержка статических файлов

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(                                           // Определение маршрутов
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
