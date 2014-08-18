namespace Mvczin.Web.Controllers
{
    using Mvczin.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Validation()
        {
            var model = new ValidationForm();

            return View(model);
        }

        [HttpPost]
        public ActionResult Validation(ValidationForm input)
        {
            if (this.ModelState.IsValid)
            {
                return this.RedirectToAction("Validation");
            }

            return View(input);
        }
    }
}