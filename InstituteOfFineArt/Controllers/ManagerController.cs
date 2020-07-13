using InstituteOfFineArt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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

        public ActionResult Exibition(exibition a)
        {
            var data = obj.exibitions.ToList();
            return View(data);
        }


        public ActionResult Create_Exibition()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create_Exibition(exibition a)
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
                    a.Image = "~/Exibition/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Exibition/"), filename);
                    a.ImageFile.SaveAs(filename);
                    obj.exibitions.Add(a);

                    int b = obj.SaveChanges();

                    if (b > 0)
                    {
                        TempData["datamessage"] = "<script>alert('Data Inserted Succesfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Exibition", "Manager");
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



        public ActionResult EditExibition(int id)
        {
            var compyrow = obj.exibitions.Where(model => model.Id == id).FirstOrDefault();
            Session["image2"] = compyrow.Image;
            return View(compyrow);
        }
        [HttpPost]
        public ActionResult EditExibition(exibition a)
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
                        a.Image = "~/Exibition/" + filename;
                        filename = Path.Combine(Server.MapPath("~/Exibition/"), filename);
                        a.ImageFile.SaveAs(filename);
                        obj.Entry(a).State = EntityState.Modified;

                        int b = obj.SaveChanges();

                        if (b > 0)
                        {
                            string imagepath = Request.MapPath(Session["image2"].ToString());
                            if (System.IO.File.Exists(imagepath))
                            {
                                System.IO.File.Delete(imagepath);
                            }
                            TempData["upmessage"] = "<script>alert('Data Updated Succesfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Exibition", "Manager");
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

        public ActionResult Exibition_Details(int? id)
        {
            var comp = obj.exibitions.Where(model => model.Id == id).FirstOrDefault();
            Session["Image2"] = comp.Image.ToString();
            return View(comp);
        }


        public ActionResult Exibition_Delete(int id)
        {
            var comp = obj.exibitions.Where(model => model.Id == id).FirstOrDefault();
            if (comp != null)
            {
                obj.Entry(comp).State = EntityState.Deleted;
                int a = obj.SaveChanges();
                if (a > 0)
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
            return RedirectToAction("Exibition", "Staff");
        }





    }
}