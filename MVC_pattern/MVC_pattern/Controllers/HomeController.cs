using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_pattern.Models;

namespace MVC_pattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MobileContext db;

        public HomeController(MobileContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Phones.ToList());
        }

        [HttpGet]
        public IActionResult Buy (int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            // ViewBag - это обьек который позволяет занести в него переменные и использовать во вьюхе 
            ViewBag.PhoneId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Buy(Order order)
        {
            db.Orders.Add(order);

            db.SaveChanges();
            //return "Спасибо, " + order.User + ", за покупку!";
            return View("afterBuy");
        }
    }
}
