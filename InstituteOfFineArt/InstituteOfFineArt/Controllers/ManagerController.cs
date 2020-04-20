using InstituteOfFineArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InstituteOfFineArt.Controllers
{
    public class ManagerController : Controller
    {
        FINE_ARTSEntities obj = new FINE_ARTSEntities();
        // GET: Manager
        public ActionResult manager()
        {
            return View();
        }

        //student
        public ActionResult Mstudent_view()
        {
            var view = obj.students.ToList();
            return View(view);
        }

        public ActionResult MStudent_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            student st = obj.students.Find(id);


            if (st == null)
            {
                return HttpNotFound();
            }

            return View(st);
        }


        public ActionResult MStudentSerch(string searchby, string search)
        {
            if (searchby == "Name")
            {
                var data2 = obj.students.Where(model => model.Name == search).ToList();
                return View(data2);
            }
            else
            {
                var data2 = obj.students.ToList();
                return View(data2);
            }

        }


        //staffs

        public ActionResult Mstaff_view()
        {
            var view = obj.staffs.ToList();
            return View(view);
        }

        public ActionResult Mstaff_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            staff st = obj.staffs.Find(id);


            if (st == null)
            {
                return HttpNotFound();
            }

            return View(st);
        }


        public ActionResult MstaffSerch(string searchby, string search)
        {
            if (searchby == "Name")
            {
                var data2 = obj.staffs.Where(model => model.Name == search).ToList();
                return View(data2);
            }
            else
            {
                var data2 = obj.staffs.ToList();
                return View(data2);
            }

        }

    }
}