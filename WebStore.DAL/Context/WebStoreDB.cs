using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;

namespace WebStore.DAL.Context
{
    class WebStoreDB : DbContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<Brand> Brands { get; set; }

        DbSet<Section> Sections { get; set; }

        public WebStoreDB(DbContextOptions<WebStoreDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
