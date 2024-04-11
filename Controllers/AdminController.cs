using System.Diagnostics;
using BuildsByBrickwellNew.Models;
using BuildsByBrickwellNew.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BuildsByBrickwellNew.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }
    }
}
