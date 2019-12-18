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
        List<Section> SecResources { get; set; } = new List<Section>(); //用于显示科室
        List<Doctor> DocResources { get; set; } = new List<Doctor>(); 

        // GET: Index
        public ActionResult Index()
        {
            IList<Section> section = onlineQEntities.Sections.ToList();
            return View();
        }

        public ActionResult IIII() {
            return View();
        }

    }
}