using InstituteOfFineArt.Models;
using System;
using System.Collections.Generic;
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

    }
}