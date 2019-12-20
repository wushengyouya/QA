﻿using QA.Models;
using QA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


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
        public ActionResult ConsultCenter(int pageIndex=1,int pageSize=5)
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

        /// <summary>
        /// 修改咨询评分
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public string ChangesPoints(string userId,int point) {
            //修改评分
            onlineQEntities.Consults.FirstOrDefault(p => p.Id == userId).points = point;
            //修改状态
            onlineQEntities.Consults.FirstOrDefault(p => p.Id == userId).state = 2;
            onlineQEntities.SaveChanges();
            return "y";
        }

        /// <summary>
        /// 显示医生编号和姓名
        /// </summary>
        /// <param name="consultInfo"></param>
        /// <returns></returns>
        public string ShowDoctors(string sectionId) {

            var consults = from s in onlineQEntities.Sections
                           join d in onlineQEntities.Doctors
                           on s.Id equals d.s_id where(s.Id==sectionId)
                           select new DoctorsTwo
                           {
                               id = d.Id,
                               d_name = d.d_name,
                           };
            return JsonConvert.SerializeObject(consults);
        }

        /// <summary>
        /// 显示医生个人简介
        /// </summary>
        /// <param name="consultInfo"></param>
        /// <returns></returns>
        public string ShowDoctorsBrief(string doctorId)
        {
            return onlineQEntities.Doctors.First(d => d.Id == doctorId).brief.ToString();
        }

        /// <summary>
        /// 添加咨询信息
        /// </summary>
        /// <param name="consultInfo"></param>
        /// <returns></returns>
        public string AddConsultInfo(string Q_describe,string p_id,string d_id) {
            var uuid = Guid.NewGuid().ToString("N");
            Consult cs = new Consult
            {
                Id = uuid,
                create_date = DateTime.Now,
                Q_describe = Q_describe,
                state = 0,
                p_id = p_id,
                d_id = d_id,
                points = null,
            };
            onlineQEntities.Consults.Add(cs);
            onlineQEntities.SaveChanges();
            return "y";
        }
    }
}