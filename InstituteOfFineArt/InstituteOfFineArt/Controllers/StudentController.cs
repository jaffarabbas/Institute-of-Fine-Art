using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstituteOfFineArt.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult student_panel()
        {
            return View();
        }
    }
}