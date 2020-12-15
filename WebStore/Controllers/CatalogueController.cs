﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IProductData _productData;

        public CatalogueController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Shop(int? brandId, int? sectionId)
        {
            var filter = new ProductFilter()
            {
                BrandId = brandId,
                SectionId = sectionId,
            };

            var products = _productData.GetProducts(filter);

            return View(new CatalogueViewModel() { 
                SectionId = sectionId,
                BrandId = brandId,
                Products = products.OrderBy(item => item.Order).Select(item => new ProductViewModel { 
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                })
            });
        }
    }
}
