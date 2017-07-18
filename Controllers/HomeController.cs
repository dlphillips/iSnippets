using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iSnippets.Models;

namespace iSnippets.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db = new MyDbContext();
        public IActionResult Index()
        {
            return View(db.SnippetTable.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
