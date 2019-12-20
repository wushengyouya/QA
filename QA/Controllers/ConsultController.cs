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

        
        //登录后创建 ---------待修改 
        private Patient CurrentUser =>  onlineQEntities.Patients.FirstOrDefault();


        public ActionResult Index()
        {
           
            IndexViewModel indexModel = new IndexViewModel
            {
                CurrentUser = CurrentUser,
                Sections = onlineQEntities.Sections
            };
           
            
            return View(indexModel);
        }

        //咨询中心
        public ActionResult ConsultCenter(int pageIndex=1,int pageSize=3)
        {
            var item = onlineQEntities.Consults.Where(p => p.p_id.Equals(CurrentUser.ID));

            //当前用户所有咨询条数
            var consults = from c in onlineQEntities.Consults
                            join d in onlineQEntities.Doctors
                            on c.d_id equals d.Id
                            select new ConsultCenterViewModel
                            {
                                CurrentDoctor = d,
                                Consult = c,
                            };

            //总页数
            ViewBag.PageCount = Math.Ceiling(consults.Count() / pageSize*1.0);

            //当前页码
            ViewBag.PageIndex = pageIndex;

            //当前页条数
            consults = consults.OrderBy(p => p.Consult.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return View(consults);
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