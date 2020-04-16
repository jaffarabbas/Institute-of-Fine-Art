using InstituteOfFineArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstituteOfFineArt.Controllers
{
    public class AdminController : Controller
    {
        FINE_ARTSEntities obj = new FINE_ARTSEntities();
        // GET: Admin
        public ActionResult admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult admin(staff a)
        {
            return View();
        }

        public ActionResult admin_s()
        {
            return View();
        }
    }
}