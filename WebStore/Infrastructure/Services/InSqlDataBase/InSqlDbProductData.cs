using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSqlDataBase
{
    public class InSqlDbProductData : IProductData
    {
        private WebStoreDB _dB;

        public InSqlDbProductData(WebStoreDB dB)
        {
            _dB = dB;
        }

        public IEnumerable<Brand> GetBrands() => _dB.Brands.Include(brand => brand.Products);

        public Product GetProductById(int id)
        {
            return _dB.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter productFilter = null) {
            IQueryable<Product> products = _dB.Products;

            if (productFilter?.BrandId != null)
                products = products.Where(p => p.BrandId == productFilter.BrandId);

            if (productFilter?.SectionId != null)
                products = products.Where(p => p.SectionId == productFilter.SectionId);

            return products;
        }

        public IEnumerable<Section> GetSections() => _dB.Sections.Include(section => section.Products);


    }
}
