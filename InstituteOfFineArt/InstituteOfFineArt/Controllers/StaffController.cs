using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InstituteOfFineArt.Models;

namespace InstituteOfFineArt.Controllers
{
    public class StaffController : Controller
    {
        FINE_ARTSEntities obj = new FINE_ARTSEntities();
        // GET: Staff
        public ActionResult Staff_pannel()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("login", "Login");
            }
            return View();
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
            if (ModelState.IsValid == true)
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

                        int data = obj.SaveChanges();

                        if(data > 0)
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