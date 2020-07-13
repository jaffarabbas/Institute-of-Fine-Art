using InstituteOfFineArt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstituteOfFineArt.Controllers
{
    public class StudentController : Controller
    {
        FINE_ARTSEntities obj = new FINE_ARTSEntities();
        // GET: Student
        public ActionResult student_panel()
        {
            return View();                                                               
        }
  
                                                                                                                                                                                                                               
        public ActionResult ViewCompitition()
        {
            var data = obj.Compititions.ToList();
            return View(data);
        }

        public ActionResult Student_Compitition_view()
        {
            var data = obj.StudentSubs.ToList();
            return View(data);
        }

        public ActionResult Create_Compitition_from_Student()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create_Compitition_from_Student(StudentSub a,Compitition c)
        {
            DateTime date = DateTime.Now;
            //string dt = Convert.ToString(date);
            //string ed = Convert.ToString(c.EndDate);
            //DateTime myDate = DateTime.ParseExact(dt, "MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture);
            //DateTime myDate2 = DateTime.ParseExact(ed, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime myd = DateTime.Today;
            DateTime myd2 = c.EndDate;
            if (myd2  <   DateTime.Today)
            {

                string filename = Path.GetFileNameWithoutExtension(a.ImageFile.FileName);
                string extension = Path.GetExtension(a.ImageFile.FileName);
                HttpPostedFileBase postedFile = a.ImageFile;
                int length = postedFile.ContentLength;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        filename = filename + extension;
                        a.image = "~/StudentImages/" + filename;
                        filename = Path.Combine(Server.MapPath("~/StudentImages/"), filename);
                        a.ImageFile.SaveAs(filename);
                        obj.StudentSubs.Add(a);

                        int b = obj.SaveChanges();

                        if (b > 0)
                        {
                            TempData["datamessage"] = "<script>alert('Data Inserted Succesfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("student_panel", "Student");
                        }
                        else
                        {
                            TempData["dtemessage"] = "<script>alert('Data Not Inserted')</script>";
                        }
                    }
                    else
                    {
                        TempData["lengthmessage"] = "<script>alert('Length should be 10 mb')</script>";
                    }
                }
                else
                {
                    TempData["extentionmessage"] = "<script>alert('Format not Supported')</script>";
                }

            }
            else
            {
                TempData["Error"] = "<script>alert('Date expired')</script>";
               
            }
            return View();
        }


        public ActionResult EditCompititionfromStudent(int id)
        {
            var compyrow = obj.StudentSubs.Where(model => model.Id == id).FirstOrDefault();
            Session["image"] = compyrow.image;
            return View(compyrow);
        }
        [HttpPost]
        public ActionResult EditCompititionfromStudent(StudentSub a)
        {

            if (a.ImageFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(a.ImageFile.FileName);
                string extension = Path.GetExtension(a.ImageFile.FileName);
                HttpPostedFileBase postedFile = a.ImageFile;
                int length = postedFile.ContentLength;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        filename = filename + extension;
                        a.image = "~/StudentImages/" + filename;
                        filename = Path.Combine(Server.MapPath("~/StudentImages/"), filename);
                        a.ImageFile.SaveAs(filename);
                        obj.Entry(a).State = EntityState.Modified;

                        int b = obj.SaveChanges();

                        if (b > 0)
                        {
                            string imagepath = Request.MapPath(Session["image"].ToString());
                            if (System.IO.File.Exists(imagepath))
                            {
                                System.IO.File.Delete(imagepath);
                            }
                            TempData["upmessage"] = "<script>alert('Data Updated Succesfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Create_Compitition_from_Student", "Student");
                        }
                        else
                        {
                            TempData["updtemessage"] = "<script>alert('Data Not Updated')</script>";
                        }
                    }
                    else
                    {
                        TempData["lengthmessage"] = "<script>alert('Length should be 10 mb')</script>";
                    }
                }
                else
                {
                    TempData["extentionmessage"] = "<script>alert('Format not Supported')</script>";
                }
            }

            return View();
        }


        public ActionResult Student_Compitition_Delete(int id)
        {
            var comp = obj.StudentSubs.Where(model => model.Id == id).FirstOrDefault();
            if (comp != null)
            {
                obj.Entry(comp).State = EntityState.Deleted;
                int a = obj.SaveChanges();
                if (a > 0)
                {
                    TempData["Delhmessage"] = "<script>alert('Data Deleted Successfully')</script>";
                    string imagepath = Request.MapPath(comp.image.ToString());
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                }
                else
                {
                    TempData["notdelmessage"] = "<script>alert('Data Not Deleted Successfully')</script>";
                }
            }
            return RedirectToAction("Student_Compitition_view", "Student");
        }



        public ActionResult Compitition_view_for_student(Compitition a)
        {
            var view = obj.Compititions.ToList();
            return View(view);
        }


        public ActionResult student_Exibition(exibition a)
        {
            var view = obj.exibitions.ToList();
            return View(view);
        }

    }
}