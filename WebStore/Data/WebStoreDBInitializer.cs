using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _webStoreDB;
        private readonly ILogger<WebStoreDbInitializer> _logger;

        public WebStoreDbInitializer(WebStoreDB webStoreDB, ILogger<WebStoreDbInitializer> logger)
        {
            this._webStoreDB = webStoreDB;
            this._logger = logger;
        }

        public void Initialize() {
            _logger.LogInformation("Старт инициализации БД");

            var dataBase = _webStoreDB.Database;

            if (dataBase.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Есть неприменённые миграции...");
                dataBase.Migrate();
                _logger.LogInformation("Миграции БД выполнены...");
            }
            else {
                _logger.LogInformation("БД в актуальном состоянии...");
            }

            try
            {
                InitializeProducts();
            }
            catch(Exception e) {
                _logger.LogError(e, "Ошибка при инициализации таблицы товаров");

                throw;
            }

            InitializeProducts();
        }

        private void InitializeProducts() {
            if (_webStoreDB.Products.Any()) {
                return;
            }

            using (_webStoreDB.Database.BeginTransaction()) {

                _webStoreDB.Sections.AddRange(TestData.Sections);

                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _webStoreDB.SaveChanges();
                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");


                _webStoreDB.Database.CommitTransaction();
            }

            using (_webStoreDB.Database.BeginTransaction())
            {

                _webStoreDB.Brands.AddRange(TestData.Brands);

                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _webStoreDB.SaveChanges();
                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");


                _webStoreDB.Database.CommitTransaction();
            }

            using (_webStoreDB.Database.BeginTransaction())
            {

                _webStoreDB.Products.AddRange(TestData.Products);

                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _webStoreDB.SaveChanges();
                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");


                _webStoreDB.Database.CommitTransaction();
            }
        }
    }
}
