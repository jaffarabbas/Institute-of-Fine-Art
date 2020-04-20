using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InstituteOfFineArt.Models;

namespace InstituteOfFineArt.Controllers
{
    public class LoginController : Controller
    {
        FINE_ARTSEntities obj = new FINE_ARTSEntities();
        // GET: Login
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(staff a, admine b,manager c,student d)
        {

            var data = obj.staffs.Where(model => model.Name == a.Name && model.Password == a.Password).FirstOrDefault();

            var data2 = obj.admines.Where(model => b.Nmae == b.Nmae && model.Password == b.Password).FirstOrDefault();

            var data3 = obj.managers.Where(model => c.Name == c.Name && model.Password == c.Password).FirstOrDefault();

            var data4 = obj.students.Where(model => d.Name == d.Name && model.Password == d.Password).FirstOrDefault();

            if (data != null)
            {
                Session["userId"] = a.Id.ToString();
                Session["Username"] = a.Name.ToString();
                TempData["LoginSuccess"] = "<script>alert('Login Succesfuly')</script>";
                return RedirectToAction("Staff_pannel", "Staff");
            }
           
            else if (data2 != null)
            {
                Session["adminId"] = a.Id.ToString();
                Session["adminname"] = a.Name.ToString();
                TempData["LoginSuccess"] = "<script>alert('Login Succesfuly')</script>";
                return RedirectToAction("admin", "Admin");
            }
            else if (data3 != null)
            {
                Session["managerId"] = c.Id.ToString();
                Session["managername"] = c.Name.ToString();
                TempData["LoginSuccess"] = "<script>alert('Login Succesfuly')</script>";
                return RedirectToAction("manager", "Manager");
            }
            else if (data4 != null)
            {
                Session["studentId"] = a.Id.ToString();
                Session["studentname"] = a.Name.ToString();
                TempData["LoginSuccess"] = "<script>alert('Login Succesfuly')</script>";
                return RedirectToAction("student_panel", "Student");
            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('Error in Login')</script>";
                return View();
            }

        }

    }
}