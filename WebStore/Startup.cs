using Microsoft.AspNetCore.Builder;
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

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreDBInitializer>();

            services.AddTransient<IEmployeesDataService, InMemoryEmployeesData>();      // Добавлен сервис для работы со списком сотрудников
            services.AddTransient<IProductData, InMemoryProductData>();
            services.AddMvc();                                                          // Добавлены сервисы MVC
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer initializer)
        {
            initializer.Initialize();

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
