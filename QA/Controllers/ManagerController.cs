using QA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.Controllers
{
    public class ManagerController : Controller
    {
        private OnlineQEntities onlineQEntities = new OnlineQEntities();
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加科室
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public ActionResult AddSection(Section section)
        {
            onlineQEntities.Sections.Add(section);
            SaveChanges();

            //跳转至所有科室界面
            return View("Section");
        }
        
        public  ActionResult UpdateSection(Section section)
        {
            onlineQEntities.Entry(section);
            SaveChanges();

            //刷新当前页面
            return View();
        }


        public ActionResult AddDoctor(Doctor doctor)
        {
            onlineQEntities.Doctors.Add(doctor);
            SaveChanges();

            //跳转至所有医生界面
            return View();
        }
        
        public ActionResult UpdateDoctor(Doctor doctor)
        {
            onlineQEntities.Entry(doctor);
            SaveChanges();

            //刷新当前修改医生信息的界面
            return View();
        }



        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return onlineQEntities.SaveChanges();
        }
    }
}