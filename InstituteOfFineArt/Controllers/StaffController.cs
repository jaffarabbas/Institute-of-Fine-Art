using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using InstituteOfFineArt.Models;

namespace InstituteOfFineArt.Controllers
{
    public class StaffController : Controller
    {
        FINE_ARTSEntities obj = new FINE_ARTSEntities();

        public EntityState EntitiyState { get; private set; }

        // GET: Staff
        public ActionResult Staff_pannel()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            return View();
        }

        public ActionResult Student_Staff_view_Admission()
        {
            var data = obj.Compititions.ToList();
            return View(data);
        }

        public ActionResult Compitition()
        {
            var data = obj.Compititions.ToList();
            return View(data);
        }

        public ActionResult Create_Compitition()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create_Compitition(Compitition a)
        {
           
                string filename = Path.GetFileNameWithoutExtension(a.ImageFile.FileName);
                string extension = Path.GetExtension(a.ImageFile.FileName);
                HttpPostedFileBase postedFile = a.ImageFile;
                int length = postedFile.ContentLength;

                if (extension.ToLower()  == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if(length <= 1000000)
                    {
                        filename = filename + extension;
                        a.Image = "~/Images/"+filename;
                        filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                        a.ImageFile.SaveAs(filename);
                        obj.Compititions.Add(a);
                        
                        int b = obj.SaveChanges();

                        if(b > 0)
                        {
                            TempData["datamessage"] = "<script>alert('Data Inserted Succesfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Create_Compitition","Staff");
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
            

            return View();
        }


        public ActionResult EditCompitition(int id)
        {
            var compyrow = obj.Compititions.Where(model => model.Id == id).FirstOrDefault();
            Session["image"] = compyrow.Image;
            return View(compyrow);
        }
        [HttpPost]
        public ActionResult EditCompitition(Compitition a)
        {
                if(a.ImageFile != null)
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
                            a.Image = "~/Images/" + filename;
                            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
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
                                return RedirectToAction("Create_Compitition", "Staff");
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

        public ActionResult Compitition_Details(int? id)
        {
            var comp = obj.Compititions.Where(model => model.Id == id).FirstOrDefault();
            Session["Image2"] = comp.Image.ToString();
            return View(comp);
        }


        public ActionResult Compitition_Delete(int id)
        {
            var comp = obj.Compititions.Where(model => model.Id == id).FirstOrDefault();
            if (comp != null)
            {
                obj.Entry(comp).State = EntityState.Deleted;
                int a = obj.SaveChanges();
                if(a > 0)
                {
                    TempData["Delhmessage"] = "<script>alert('Data Deleted Successfully')</script>";
                    string imagepath = Request.MapPath(comp.Image.ToString());
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
            return RedirectToAction("Compitition","Staff");
        }



        public ActionResult Compitition_view_for_student(Compitition a)
        {
            var view = obj.Compititions.ToList();
            return View(view);
        }
    
        public ActionResult AwardList(Compitition a)
        {
            var data = obj.Compititions.ToList();
            return View(data);
        }

        public ActionResult SelectWinners(StudentSub a)
        {
            var data = obj.StudentSubs.SingleOrDefault();
            return View(data);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("login", "Login");
        }
    }
}