﻿using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework.XamlTypes;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {

        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        

        public ViewResult List(string Category, int productPage = 1)
         => View(new ProductListViewModel
         {
             Products = repository.Products
                                   .Where(p => Category == null || p.Category == Category)
                                   .OrderBy(p => p.ProductID)
                                   .Skip((productPage - 1) * PageSize)
                                   .Take(PageSize),
             PagingInfo = new PagingInfo
             {
                 CurrentPage = productPage,
                 ItemsPerPage = PageSize,
                 TotalItems = Category == null ?
                    repository.Products.Count() :
                    repository.Products.Where(e =>
                        e.Category == Category).Count()
             },
             CurrentCategory = Category
         });

    }
}