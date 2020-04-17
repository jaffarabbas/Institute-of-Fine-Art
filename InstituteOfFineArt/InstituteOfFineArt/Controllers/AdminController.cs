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
            if(ModelState.IsValid == true)
            {
                obj.staffs.Add(a);
                int taker = obj.SaveChanges();
                if(taker > 0)
                {
                    ViewBag.InsertMessage = "<script>alert('Regestred Succesfuly')</script>";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Error in Regestration')</script>";
                }
            }
            return View();
        }

        public ActionResult admin_s()
        {
            return View();
        }
        [HttpPost]
        public ActionResult admin_s(student a)
        {
            return View();
        }
    }
}