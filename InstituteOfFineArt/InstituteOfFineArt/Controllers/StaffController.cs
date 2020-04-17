using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InstituteOfFineArt.Models;

namespace InstituteOfFineArt.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Staff_pannel()
        {
            return View();
        }
    }
}