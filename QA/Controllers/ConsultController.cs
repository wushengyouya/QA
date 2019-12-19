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

        //咨询中心
        public ActionResult ConsultCenter() {
            return View();
        }

        //个人中心
        public ActionResult PersonCenter(string id)
        {
            Patient patientItem = onlineQEntities.Patients.FirstOrDefault(p => p.ID.Equals(id));
            return View(patientItem);
        }

    }
}