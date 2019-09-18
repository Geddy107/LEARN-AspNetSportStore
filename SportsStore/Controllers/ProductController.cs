﻿using Microsoft.AspNetCore.Mvc;
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
                 TotalItems = repository.Products.Count()
             },
             CurrentCategory = Category
         });

    }
}