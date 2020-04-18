using InstituteOfFineArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (Session["adminId"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            else
            {
                return View();
            }

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
            if (ModelState.IsValid == true)
            {
                obj.students.Add(a);
                int taker = obj.SaveChanges();
                if (taker > 0)
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

        public ActionResult Staff_view()
        {
            var view = obj.staffs.ToList();
            return View(view);
        }

        public ActionResult Staff_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            staff staffs = obj.staffs.Find(id);

            if (staffs == null)
            {
                return HttpNotFound();
            }

            return View(staffs);
        }
        
        public ActionResult StaffSerch(string serchby,string search)
        {
            if (serchby == "Name")
            {
                var data = obj.staffs.Where(model => model.Name == search).ToList();
                return View(data);
            }
            else
            {
                var data = obj.staffs.ToList();
                return View(data);
            }
   
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("login", "Login");
        }
    }
}