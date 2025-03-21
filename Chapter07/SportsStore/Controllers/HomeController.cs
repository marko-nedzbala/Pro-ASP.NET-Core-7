﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;

        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        //public IActionResult Index()
        //{
        //    return View(repository.Products);
        //}
        public IActionResult Index() => View(repository.Products);
    }
}
