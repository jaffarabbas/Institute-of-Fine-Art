using InstituteOfFineArt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult AdminStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminStudent(student b)
        {
            if (ModelState.IsValid == true)
            {
                obj.students.Add(b);
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

        public ActionResult Staff_Delete(int? id)
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

        // POST: Students/Delete/5
        [HttpPost, ActionName("Staff_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            staff staffs = obj.staffs.Find(id);
            obj.staffs.Remove(staffs);
            obj.SaveChanges();
            return RedirectToAction("Staff_view");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                obj.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Students/Edit/5
        public ActionResult Staff_Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staff data = obj.staffs.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Staff_Edit([Bind(Include = "Id,Name,Email,Number,gender,Qualification,Class,Subject,Password,Co_Password")] staff data)
        {
            if (ModelState.IsValid)
            {
                obj.Entry(data).State = EntityState.Modified;
                ViewBag.InsertMessage = "<script>alert('Edit Succesfuly')</script>";
                obj.SaveChanges();
               
                return RedirectToAction("Staff_view");
               
            }
            return View();
        }



        //STUDENT


        public ActionResult Student_view()
        {
            var view = obj.students.ToList();
            return View(view);
        }

        public ActionResult Student_Details(int? id)
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

        public ActionResult StudentSerch(string searchby, string search)
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

        public ActionResult Student_Delete(int? id)
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

        // POST: Students/Delete/5
        [HttpPost, ActionName("Student_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult StudentDelConfirmed(int id)
        {
            student st = obj.students.Find(id);
            obj.students.Remove(st);
            obj.SaveChanges();
            return RedirectToAction("Student_view");
        }




        // GET: Students/Edit/5
        public ActionResult Student_Edit(int? id)
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

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Student_Edit([Bind(Include = "Id,Name,Email,age,gender,Class,Subject,Password,Co_Password")] student sa)
        {
            if (ModelState.IsValid)
            {
                obj.Entry(sa).State = EntityState.Modified;
                ViewBag.InsertMessage = "<script>alert('Edit Succesfuly')</script>";
                obj.SaveChanges();

                return RedirectToAction("Student_view");

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