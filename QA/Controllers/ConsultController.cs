using QA.Models;
using QA.ViewModels;
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

        
        
        public ActionResult Index()
        {
            //登录后创建 ---------待修改
            Session["currentUser"] = onlineQEntities.Patients.FirstOrDefault();
            IndexModel indexModel = new IndexModel
            {
                CurrentUser = (Patient)Session["currentUser"],
                Sections = onlineQEntities.Sections
            };
           
            
            return View(indexModel);
        }

        //咨询中心
        public ActionResult ConsultCenter() {
            return View();
        }

        /// <summary>
        /// 患者个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        public ActionResult PersonCenter()
        {
            string url = Request.Url.AbsoluteUri;
            string userId = Request["userId"];
            var patient = onlineQEntities.Patients.FirstOrDefault(p => p.ID == userId);
            return View(patient);
        }




    }
}