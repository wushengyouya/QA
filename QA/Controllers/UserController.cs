using QA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.Controllers
{
    public class UserController : Controller
    {
        private OnlineQEntities onlineQEntities = new OnlineQEntities();
        // GET: Login

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string userAccount,string userPwd)
        {
            //病人登录          
            var patientInfo = onlineQEntities.Patients.FirstOrDefault(p => p.p_account.Equals(userAccount));
            if(patientInfo !=null && patientInfo.p_account.Equals(userAccount) && patientInfo.Password.Equals(userPwd))
            {
                //跳转至选择科室界面,目前展示的是个人信息界面
                return Redirect("/Consult/Index");
            }
            
                   
           

            //医生登录
            var doctorInfo = onlineQEntities.Doctors.FirstOrDefault(d => d.d_account.Equals(userAccount));
            if(doctorInfo!=null && doctorInfo.d_account.Equals(userAccount) && doctorInfo.password.Equals(userPwd))
            {
                //跳转至回答咨询界面
                return View("DoctorPersonal");
            }
            ViewBag.Msg = "用户名或密码错误";
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        public ActionResult Register(Patient patient)
        {
            if (patient.p_account!=null){
                patient.ID = Guid.NewGuid().ToString("N");
                patient.Enroll_date = DateTime.Now;
                patient.Birthday = DateTime.Now;
                onlineQEntities.Patients.Add(patient);
                if (SaveChanges() > 0)
                {             
                    return Redirect("/Consult/Index");                   
                }
            }
            ViewBag.Msg = "未知错误";
            return View();
        }

        /// <summary>
        /// 医生个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DoctorInfo(string id)
        {
            var doctor = onlineQEntities.Doctors.First(d => d.Id == id);
            return Json(doctor);
        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        public string UpdatePatientInfo(string userId,string newpas)
        {
            onlineQEntities.Patients.FirstOrDefault(p => p.ID == userId).Password=newpas;
            SaveChanges();
            return "y";
        }

        

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
           return  onlineQEntities.SaveChanges();
        }
    }
}