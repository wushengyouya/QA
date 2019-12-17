using QA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.Controllers
{

    public class ConsultController : Controller
    {
        private OnlineQEntities onlineQEntities = new OnlineQEntities();
        // GET: Index
        public ActionResult Index()
        {
            IList<Doctor> doctors = onlineQEntities.Doctors.ToList();
            return View(doctors);
        }

        public ActionResult IIII() {
            return View();
        }

    }
}