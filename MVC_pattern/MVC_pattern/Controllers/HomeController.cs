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
    [Controller]
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

        [HttpGet]
        // Можно задать имя действия таким образом. По умолчанию это название метода
        //[ActionName("Ainur")]
        public IActionResult Practice(string regex, int number)
        {
            return Redirect("~/Home/Kitty?number=1234");
            //return "Вы выбрали: " + regex + " под номером: " + number;
        }

        [HttpGet]
        public IActionResult Kitty(int number)
        {
            string src = "";
            if (number == 1234)
            {
                src = "https://klike.net/uploads/posts/2018-10/1539499416_1.jpg";
            }
            else
            {
                src = "https://envato-shoebox-0.imgix.net/044c/8297-cbb7-4a1e-95c3-299ba3071dda/0O1A3636.JPG?auto=compress%2Cformat&fit=max&mark=https%3A%2F%2Felements-assets.envato.com%2Fstatic%2Fwatermark2.png&markalign=center%2Cmiddle&markalpha=18&w=700&s=7dca16e00bb5fb37d7ee6541868943f5";
            }
            ViewBag.src = src;
            return View();
        }

        public JsonResult GetJson()
        {
            Phone phone = db.Phones.ToList()[0];
            return Json(phone);
        }

    }
}
