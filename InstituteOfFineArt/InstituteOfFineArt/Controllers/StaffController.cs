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
            if (Session["userId"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("login", "Login");
        }
    }
}