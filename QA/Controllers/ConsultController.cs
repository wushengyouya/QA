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
        // GET: Index
        public ActionResult Index()
        {
            OnlineQEntities onlineQEntities = new OnlineQEntities();
            IList<Doctor> doctors = onlineQEntities.Doctors.ToList();

            return View(doctors);
        }
    }
}