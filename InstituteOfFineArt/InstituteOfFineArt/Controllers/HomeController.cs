using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstituteOfFineArt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Slider()
        {
            return View();
        }
        public ActionResult admin_p()
        {
            return View();
        }
    }
}