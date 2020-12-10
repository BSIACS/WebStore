using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Product> GetProducts(ProductFilter productFilter)
        {
            var query = TestData.Products;

            if (productFilter?.SectionId is { } section_Id)
                query = query.Where(product => product.SectionId == section_Id);

            if (productFilter?.BrandId != null)
                query = query.Where(product => product.BrandId == productFilter.BrandId);

            return query;
        }
    }
}
